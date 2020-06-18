using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TinyEdu.Common.Dapper.DI;
using TinyEdu.Common.Dapper.Language;
using TinyEdu.Common.Dapper.Persistence.Data;
using TinyEdu.Common.Dapper.Validate;

namespace TinyEdu.Common.Dapper.Entity
{
    /// <summary>
    /// 基础对象
    /// </summary>
    public class BaseInfo
    {
        /// <summary>
        /// 自增长主键值，无实际意义，只是为了新增时拿到当前的自增长主键值
        /// </summary>
        [NoMapper]
        public long AutoIncrementId { set; get; }
        ///// <summary>
        ///// 唯一标识
        ///// </summary>
        //[Key]
        //public long Id { set; get; }
        ///// <summary>
        ///// 创建时间
        ///// </summary>
        //public DateTime CreatedDate { set; get; }
        ///// <summary>
        ///// 更新时间
        ///// </summary>
        //public DateTime? UpdateDate { set; get; }
        ///// <summary>
        ///// 版本号
        ///// </summary>
        //public long Version { set; get; }
        /// <summary>
        /// 保存类型
        /// </summary>
        [NoMapper]
        public SaveType SaveType { set; get; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [NoMapper]
        public virtual IList<ErrorInfo> Errors { get; set; }

        #region 通用方法
        /// <summary>
        /// 添加错误信息
        /// </summary>
        public virtual void AddError(ErrorInfo error)
        {
            Errors = Errors ?? new List<ErrorInfo>();
            Errors.Add(error);
        }
        /// <summary>
        /// 添加错误信息列表
        /// </summary>
        /// <param name="errors"></param>
        public virtual void AddErrors(IList<ErrorInfo> errors)
        {
            Errors = Errors ?? new List<ErrorInfo>();
            errors?.ToList().ForEach(error =>
            {
                Errors.Add(error);
            });
        }

        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="paramters"></param>
        public virtual void AddError(string propertyName, params object[] paramters)
        {
            AddErrorByName(GetType().FullName, propertyName, paramters);
        }

        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propertyName"></param>
        /// <param name="paramters"></param>
        public virtual void AddErrorByName(string name, string propertyName, params object[] paramters)
        {
            Errors = Errors ?? new List<ErrorInfo>();
            Errors.Add(GetErrorByName(name, propertyName, paramters));
        }

        /// <summary>
        /// 得到错误
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propertyName"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public virtual ErrorInfo GetErrorByName(string name, string propertyName, params object[] paramters)
        {
            var error = IoC.Resolve<IValidation>().GetErrorInfo(name, propertyName);
            error.Message = string.Format(error.Message, paramters);
            return error;
        }

        /// <summary>
        /// 得到错误
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public virtual ErrorInfo GetError(string propertyName, params object[] paramters)
        {
            return GetErrorByName(GetType().FullName, propertyName, paramters);
        }

        /// <summary>
        /// 获取错误文本信息
        /// </summary>
        /// <param name="joinChar"></param>
        /// <returns></returns>
        public virtual string GetErrors(string joinChar = ",")
        {
            return Errors == null ? string.Empty : string.Join(",", Errors.Select(it => it.Message).ToArray());
        }
        /// <summary>
        /// 得到名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual string GetLanguage<T>(object value)
        {
            return IoC.Resolve<ILanguage>().GetName($"{typeof(T).FullName}", value.ToString());
        }

        /// <summary>
        /// 得到名称
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual string GetLanguage(string propertyName, object value)
        {
            return IoC.Resolve<ILanguage>().GetName($"{GetType().FullName}.{propertyName}", value.ToString());
        }
        #endregion

        public bool Validate()
        {
            var properties = GetType().GetProperties()
                                 .Where(it => it.GetCustomAttribute(typeof(NoMapperAttribute)) as NoMapperAttribute == null)
                                 .Select(it => it.Name).ToList();
            if (SaveType != SaveType.None)
            {
                IDictionary<SaveType, ValidationType> temp = new Dictionary<SaveType, ValidationType>
                    {
                    { SaveType.Add,ValidationType.Add },{ SaveType.Modify,ValidationType.Modify },{ SaveType.Remove,ValidationType.Remove }
                };
                Errors = IoC.Resolve<IValidation>().ValidateInfo(this, temp[SaveType], properties);
            }
            return Errors == null || Errors.Count == 0;
        }
    }
}
