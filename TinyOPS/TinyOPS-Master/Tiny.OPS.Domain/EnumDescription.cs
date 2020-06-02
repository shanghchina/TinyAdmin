
using System;
using System.ComponentModel;
using System.Reflection;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 枚举描述
    /// </summary>
   public static class EnumDescription
    {
        ///// <summary>
        ///// 获取枚举描述
        ///// </summary>
        ///// <param name="enumValue"></param>
        ///// <returns></returns>
        //public static string GetEnumDescription(this Enum em)
        //{
        //    Type type = em.GetType();
        //    FieldInfo fd = type.GetField(em.ToString());
        //    if (fd == null)
        //        return string.Empty;
        //    object[] attrs = fd.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //    string name = string.Empty;
        //    foreach (DescriptionAttribute attr in attrs)
        //    {
        //        name = attr.Description;
        //    }
        //    return name;
        //}
    }
}
