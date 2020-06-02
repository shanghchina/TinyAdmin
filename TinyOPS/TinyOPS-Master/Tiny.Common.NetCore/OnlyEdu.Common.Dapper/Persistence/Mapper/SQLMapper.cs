using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using OnlyEdu.Common.Dapper.Persistence.Data;

namespace OnlyEdu.Common.Dapper.Persistence.Mapper
{
    public class SQLMapper : IMapper
    {
        public IDbConnection GetConnection(string connectionStr)
        {
            return new SqlConnection(connectionStr);
        }


        public IDbCommand CreateCommandParams(IDbCommand command, DataParameterInfo[] paramsInfo)
        {
            if (paramsInfo != null && paramsInfo.Length > 0)
            {
                foreach (var parameter in paramsInfo)
                {
                    var sqlParam = new SqlParameter
                    {
                        Direction = parameter.Direction,
                        ParameterName = parameter.ParameterName,
                        IsNullable = parameter.IsNullable,
                        Scale = parameter.Scale,
                        Value = parameter.Value,
                        Size = parameter.Size,
                        Precision = parameter.Precision,
                        SqlDbType = parameter.SqlDbType
                    };
                    command.Parameters.Add(sqlParam);
                }
            }
            return command;
        }

        protected IList<string> GetEntityProperties(Type type)
        {
            //没加nomapper且没key特性的,或者有key且非自增的
            return type.GetProperties()
                                .Where(it => (it.GetCustomAttribute(typeof(NoMapperAttribute), true) == null&& it.GetCustomAttribute(typeof(KeyAttribute), true) == null)
                                             || (it.GetCustomAttribute(typeof(KeyAttribute), true) != null && !it.GetCustomAttribute<KeyAttribute>(true).IsIgnore))
                                .Select(it => it.Name).ToList();
        }
        protected string GetEntityKey(Type type)
        {
            var propertyInfo = type.GetProperties().FirstOrDefault(it => it.GetCustomAttribute<KeyAttribute>(true) != null);
            if (propertyInfo == null)
                throw new Exception($"{type.FullName}中的字段必须带有KeyAttribute主键标识");
            return propertyInfo.Name;
        }

        public string GetNewCommand(Type type)
        {
            var tableAtt = type.GetCustomAttribute(typeof(TableAttribute), true) as TableAttribute;
            if (tableAtt == null) throw new Exception("UnValid BaseInfo, Not Set TableName Attribute");
            var cols = GetEntityProperties(type).ToList().Select(it => $"[{it}]");
            var values = cols.ToList().Select(it => $"@{it}".Replace("[", "").Replace("]", ""));
            return $"insert into {tableAtt.TableName} ({string.Join(",", cols.ToArray())}) values ({string.Join(",", values.ToArray())});SELECT CAST(SCOPE_IDENTITY() as bigint);";
        }

        public string GetUpdateCommand(Type type)
        {
            var key = GetEntityKey(type);
            var tableAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            if (tableAtt == null) throw new Exception("UnValid BaseInfo, Not Set TableName Attribute");
            var props = string.Join(",", GetEntityProperties(type).ToList().Where(it => it.ToLower() != "id").Select(it => $"[{it}] = @{it}").ToArray());
            return $"update {tableAtt.TableName} set {props} where {key} = @{key}";
        }

        public string GetRemoveCommand(Type type)
        {
            var key = GetEntityKey(type);
            var tableAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            if (tableAtt == null) throw new Exception("UnValid BaseInfo, Not Set TableName Attribute");
            return $"delete from {tableAtt.TableName} where {key} = @{key}";
        }

        public string GetPageCommand(Type type, string filter, string order, int pageIndex, int pageSize)
        {
            if (string.IsNullOrEmpty(filter))
                filter = "1=1";
            var tableAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            if (tableAtt == null) throw new Exception("UnValid BaseInfo, Not Set TableName Attribute");
            return $"select * from (select *, ROW_NUMBER() over(order by {order}) as rows from {tableAtt.TableName} where {filter}) as c where c.rows >= {(pageIndex - 1) * pageSize + 1} and c.rows <= {pageIndex * (pageSize)}";
        }
        public string GetPageCommandByMultilist(string sql,string order , int pageIndex, int pageSize)
        {
            return $"{sql} order by {order} offset {(pageIndex - 1) * pageSize} row fetch next {pageSize} rows only";
        }

        public string GetPageCommandByMultilist(string sql, int pageIndex, int pageSize)
        {
            return $"{sql} offset {(pageIndex - 1) * pageSize} row fetch next {pageSize} rows only";
        }

        public string GetPageCount(Type type, string filter)
        {
            if (string.IsNullOrEmpty(filter))
                filter = "1=1";
            var tableAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            if (tableAtt == null) throw new Exception("UnValid BaseInfo, Not Set TableName Attribute");
            return $"select count(1) from {tableAtt.TableName} where {filter}";
        }
        public string GetPageCountByMultilist(string froms, string filter)
        {
            if (string.IsNullOrEmpty(filter)) filter = "1=1";
            return $"select count(1) from {froms} where {filter}";
        }

        public string GetKeyCommand(Type type)
        {
            var key = GetEntityKey(type);
            var tableAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            if (tableAtt == null) throw new Exception("UnValid BaseInfo, Not Set TableName Attribute");
            return $"select * from {tableAtt.TableName} where {key} = @Key";
        }

    }
}
