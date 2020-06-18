using TinyEdu.Common.Dapper.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TinyEdu.Common.Dapper.Repository
{
    public class BatchInsertHelper<T>
    {
        public BatchInsertHelper() { }
        public BatchInsertHelper(string connectionString)
        {
            ConnectString = connectionString;
        }
        private string ConnectString { get; set; } = DataBaseHelper.GetConnectionStrings(Enumeration.EumDBWay.Writer, "", "");
        private const int BatchCount = 200000;
        public void AddBatch(IEnumerable<T> List)
        {
            try
            {
                if (List == null || !List.Any())
                    return;
                string @table = typeof(T).GetCustomAttributes(false).OfType<TableAttribute>().LastOrDefault().TableName;

                var _count = Math.Ceiling(Convert.ToDecimal(List.Count()) / BatchCount);
                for (int i = 0; i < _count; i++)
                {
                    var _batch = List.Skip(i * BatchCount).Take(BatchCount);

                    using (SqlBulkCopy sbc = new SqlBulkCopy(ConnectString))
                    {
                        sbc.BulkCopyTimeout = int.MaxValue;
                        //设置要插入的表名
                        sbc.DestinationTableName = @table;
                        //创建本地数据集合
                        var dt = EnumerableToTable(_batch, sbc);
                        sbc.WriteToServer(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable EnumerableToTable(IEnumerable<T> List, SqlBulkCopy sbc)
        {
            DataTable dt = new DataTable();
            var _properties = typeof(T).GetProperties();
            if (!BatchInsertHelper_Extention.ExtentionList.ContainsKey(typeof(T)))
            {
                //熏黄当前对象属性
                foreach (var property in _properties)
                {
                    Type columnType = property.PropertyType;
                    //排除主键属性以及NoMapping属性
                    if (!property.CustomAttributes.Any(x => x.AttributeType == typeof(KeyAttribute)) && !property.CustomAttributes.Any(x => x.AttributeType == typeof(NoMapperAttribute)))
                    {
                        //如果是Nullable属性，获取其根属性
                        if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            columnType = property.PropertyType.GetGenericArguments()[0];
                        //创建列
                        dt.Columns.Add(property.Name, columnType);
                        //与服务器数据库列名映射，
                        sbc.ColumnMappings.Add(property.Name, property.Name);
                    }
                }
                foreach (var item in List)
                {
                    DataRow row = dt.NewRow();
                    foreach (var property in _properties)
                    {
                        if (!property.CustomAttributes.Any(x => x.AttributeType == typeof(KeyAttribute)) && !property.CustomAttributes.Any(x => x.AttributeType == typeof(NoMapperAttribute)))
                        {
                            object obj = property.GetValue(item);
                            if (obj != null)
                                row[property.Name] = obj;
                            else
                                row[property.Name] = DBNull.Value;

                        }
                    }
                    dt.Rows.Add(row);
                }
            }
            else
            {
                MethodInfo _convert = typeof(BatchInsertHelper_Extention).GetMethod(BatchInsertHelper_Extention.ExtentionList[typeof(T)]);
                dt = (DataTable)_convert.Invoke(null, new object[] { List, sbc });
            }
            return dt;
        }
    }

    public static class BatchInsertHelper_Extention
    {
        private static Dictionary<Type, string> _ExtentionList { get; set; }
        public static Dictionary<Type, string> ExtentionList
        {
            get
            {
                if (_ExtentionList == null)
                {
                    _ExtentionList = new Dictionary<Type, string>();
                    //_ExtentionList.Add(typeof(IncomeAccrualRemainDetail), "ConvertIncomeAccrualRemainDetail");
                    //_ExtentionList.Add(typeof(IncomeAccrualProcessDetail), "ConvertIncomeAccrualProcessDetail");
                    //_ExtentionList.Add(typeof(IncomeAccrualUsedClassHourDetail), "ConvertIncomeAccrualUsedClassHourDetail");
                }
                return _ExtentionList;
            }
        }
    }
}
