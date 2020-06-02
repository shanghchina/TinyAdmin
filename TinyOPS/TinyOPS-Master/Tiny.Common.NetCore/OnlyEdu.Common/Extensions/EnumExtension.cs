using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;

namespace OnlyEdu.Common.Extensions
{
    /// <summary>
    /// 2014-06-10
    /// 枚举扩展
    /// </summary>
    public static class EnumExtension
    {
        public const String EnumValueField = "Value";
        public const String EnumNameField = "Name";
        public const String EnumDescriptionField = "Description";
        public static ICollection<EnumObject> GetEnumObject(Type type)
        {
            ICollection<EnumObject> lst = new List<EnumObject>();

            if (type == null) return lst;
            FieldInfo[] fields = type.GetFields();
            Int32 count = fields.Length;
            for (Int32 i = 0; i < count; i++)
            {
                if (fields[i].Name == "value__")
                {
                    continue;
                }
                EnumObject enumObject = new EnumObject();
                FieldInfo field = fields[i];
                //值列
                try
                {
                    enumObject.Value = ((Int32)Enum.Parse(type, field.Name)).ToString();
                }
                catch
                {
                    enumObject.Value = Int32.MinValue.ToString();
                }
                //Enum.t
                //enum字段名称
                enumObject.Name = field.Name;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    enumObject.Description = field.Name;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    enumObject.Description = da.Description;
                }

                lst.Add(enumObject);
            }
            return lst;
        }
        /// <summary>
        /// 把结构转换成DataTable数据集,两个例分别为Name与Value,可通过本类的GetEnumDtValueField字段获取ByArk2009-2-12,16:40(星期四)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetEnumDataTable(Type type)
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add(EnumValueField, typeof(Int32));
            dtbl.Columns.Add(EnumDescriptionField, typeof(String));
            dtbl.Columns.Add(EnumNameField, typeof(String));
            DataRow drow;

            FieldInfo[] fields = type.GetFields();
            Int32 count = fields.Length;
            for (Int32 i = 0; i < count; i++)
            {
                if (fields[i].Name == "value__")
                {
                    continue;
                }
                drow = dtbl.NewRow();
                FieldInfo field = fields[i];
                //值列
                try
                {
                    drow[0] = ((Int32)Enum.Parse(type, field.Name)).ToString();
                }
                catch
                {
                    drow[0] = Int32.MinValue;
                }
                //Enum.t
                //enum字段名称
                drow[2] = field.Name;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    drow[1] = field.Name;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    drow[1] = da.Description;
                }

                dtbl.Rows.Add(drow);
            }
            return dtbl;
        }
        /// <summary>
        /// 2014-8-7
        /// 获得枚举值列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<EnumObject> GetEnumList(Type type)
        {
            List<EnumObject> lst = new List<EnumObject>();
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add(EnumValueField, typeof(Int64));
            dtbl.Columns.Add(EnumDescriptionField, typeof(String));
            dtbl.Columns.Add(EnumNameField, typeof(String));
            EnumObject drow;

            FieldInfo[] fields = type.GetFields();
            Int32 count = fields.Length;
            for (Int32 i = 0; i < count; i++)
            {
                if (fields[i].Name == "value__")
                {
                    continue;
                }
                drow = new EnumObject();
                FieldInfo field = fields[i];
                //值列
                try
                {
                    drow.Value = ((Int64)Enum.Parse(type, field.Name)).ToString();
                }
                catch
                {
                    drow.Value = Int64.MinValue.ToString();
                }
                //Enum.t
                //enum字段名称
                drow.Name = field.Name;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    drow.Description = field.Name;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    drow.Description = da.Description;
                }

                lst.Add(drow);
            }
            return lst;
        }

        public static List<EnumObject> GetEnumInt32List(Type type)
        {
            List<EnumObject> lst = new List<EnumObject>();
            //DataTable dtbl = new DataTable();
            //dtbl.Columns.Add(EnumValueField, typeof(Int32));
            //dtbl.Columns.Add(EnumDescriptionField, typeof(String));
            //dtbl.Columns.Add(EnumNameField, typeof(String));
            EnumObject drow;

            FieldInfo[] fields = type.GetFields();
            Int32 count = fields.Length;
            for (Int32 i = 0; i < count; i++)
            {
                drow = new EnumObject();
                if (fields[i].Name == "value__")
                {
                    continue;
                }
                drow = new EnumObject();
                FieldInfo field = fields[i];
                //值列
                try
                {
                    drow.Value = ((Int32)Enum.Parse(type, field.Name)).ToString();
                }
                catch
                {
                    drow.Value = Int32.MinValue.ToString();
                }
                //Enum.t
                //enum字段名称
                drow.Name = field.Name;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    drow.Description = field.Name;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    drow.Description = da.Description;
                }

                lst.Add(drow);
            }
            return lst;
        }
        public static DataTable GetEnumDataTableForByteValue(Type type)
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add(EnumValueField, typeof(Byte));
            dtbl.Columns.Add(EnumDescriptionField, typeof(String));
            dtbl.Columns.Add(EnumNameField, typeof(String));
            DataRow drow;

            FieldInfo[] fields = type.GetFields();
            Int32 count = fields.Length;
            for (Int32 i = 0; i < count; i++)
            {
                if (fields[i].Name == "value__")
                {
                    continue;
                }
                drow = dtbl.NewRow();
                FieldInfo field = fields[i];
                //值列
                try
                {
                    drow[0] = ((Byte)Enum.Parse(type, field.Name)).ToString();
                }
                catch
                {
                    drow[0] = Byte.MinValue;
                }
                //Enum.t
                //enum字段名称
                drow[2] = field.Name;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    drow[1] = field.Name;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    drow[1] = da.Description;
                }

                dtbl.Rows.Add(drow);
            }
            return dtbl;
        }
        /// <summary>
        /// 2009-2-6
        /// 取得枚举值描述信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static String ToEnumDescription(this Object e)
        {
            String tmpstr = e.ToString();
            //获取字段信息   
            FieldInfo ms = e.GetType().GetField(e.ToString());

            if (ms != null)
            {
                Object[] objs = ms.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

                if (objs != null && objs.Length != 0)
                {
                    System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
                    tmpstr = da.Description;
                }
            }
            return tmpstr;
        }
        public static String ToEnumTypeDescription(this Type e)
        {
            String tmpstr = e.ToString();
            //获取字段信息   
            FieldInfo ms = e.GetField(e.ToString());

            if (ms != null)
            {
                Object[] objs = ms.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

                if (objs != null && objs.Length != 0)
                {
                    System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
                    tmpstr = da.Description;
                }
            }
            return tmpstr;
        }
        public static String ToEnumTypeName(this Object e)
        {
            String tmpstr = e.ToString();
            //获取字段信息   
            FieldInfo ms = e.GetType().GetField(e.ToString());


            if (ms != null)
            {
                tmpstr = ms.Name;
            }
            return tmpstr;
        }
        public static String ToEnumTypeName(this Object e, Type type)
        {
            return Enum.Parse(type, e.ToString()).ToEnumTypeName();
        }
        public static String ToEnumTypeDescription(this Object e, Type type)
        {
            return Enum.Parse(type, e.ToString()).ToEnumDescription();

        }
        public static String GetEnumTypeName(Object e)
        {
            String tmpstr = e.ToString();
            //获取字段信息   
            FieldInfo ms = e.GetType().GetField(e.ToString());


            if (ms != null)
            {
                tmpstr = ms.Name;
            }
            return tmpstr;
        }
        public static String ToEnumTypeDescriptionByID(Type enumtype, string value)
        {
            string tmpstr = string.Empty;
            tmpstr = ToEnumDescription(Enum.Parse(enumtype, value));
            return tmpstr;
        }
        /// <summary>
        /// 把枚举值设置到对应的列中2009-2-19,15:07(星期四)
        /// </summary>
        /// <param name="drow">要设置的Dr</param>
        /// <param name="fieldName">取得值的列</param>
        /// <param name="NewfieldName">枚举值存放列</param>
        /// <param name="type">type</param>
        public static void SetDrowTextShort(DataRow drow, string fieldName, string NewfieldName, Type type)
        {
            FieldInfo[] fields = type.GetFields();
            String enumvalue;
            String Description;
            FieldInfo field1;
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                field1 = fields[i];
                Description = field1.Name;
                try
                {
                    enumvalue = ((short)Enum.Parse(type, field1.Name)).ToString();
                }
                catch
                {
                    enumvalue = String.Empty;
                }
                if (drow[fieldName].ToString().Equals(enumvalue))
                {
                    object[] objs = field1.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (objs != null && objs.Length > 0)
                    {
                        Description = ((DescriptionAttribute)objs[0]).Description;
                    }
                    drow[NewfieldName] = Description;
                    break;
                }
            }
        }
        //
    }
    [DataContract()]
    public class EnumObject
    {
        [DataMember]
        public String Name;
        [DataMember]
        public String Value;
        [DataMember]
        public String Description;
    }
}
