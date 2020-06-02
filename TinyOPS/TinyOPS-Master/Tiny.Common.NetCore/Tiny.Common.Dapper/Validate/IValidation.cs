using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.Common.Dapper.Validate
{
    public interface IValidation
    {
        string ConfigFile { set; get; }
        /// <summary>
        /// 验证对象
        /// </summary>
        /// <param name="info"></param>
        /// <param name="type"></param>
        /// <param name="propertys"></param>
        /// <returns></returns>
        IList<ErrorInfo> ValidateInfo(object info, ValidationType type, IList<string> propertys = null);

        /// <summary>
        /// 验证对象
        /// </summary>
        /// <param name="info"></param>
        /// <param name="valids"></param>
        /// <param name="type"></param>
        /// <param name="propertys"></param>
        /// <returns></returns>
        IList<ErrorInfo> ValidateInfo(object info, IList<ValidationInfo> valids, ValidationType type, IList<string> propertys = null);
        /// <summary>
        /// 得到验证信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IList<ValidationInfo> GetValidations(string name);
        /// <summary>
        /// 得到验证信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<ValidationInfo> GetValidations(Type type);
        /// <summary>
        /// 按属性名得到验证信息
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        ValidationInfo GetValidations(string typeName, string propertyName);
        /// <summary>
        /// 添加验证信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="infos"></param>
        /// <returns></returns>
        bool AddValidations(string name, IList<ValidationInfo> infos);
        /// <summary>
        /// 得到错误信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propertName"></param>
        /// <returns></returns>
        ErrorInfo GetErrorInfo(string name, string propertName);
    }
}
