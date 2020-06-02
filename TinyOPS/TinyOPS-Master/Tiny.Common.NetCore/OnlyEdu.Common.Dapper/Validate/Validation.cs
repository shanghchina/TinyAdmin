﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OnlyEdu.Common.Dapper.Validate
{
    public class Validation : IValidation
    {
        #region 属性
        public virtual string ConfigFile { set; get; }
        /// <summary>
        /// 验证信息
        /// </summary>
        public IDictionary<string, IList<ValidationInfo>> Validations { get; set; } = new Dictionary<string, IList<ValidationInfo>>();

        #endregion

        #region 接口的实现

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="info"></param>
        /// <param name="type"></param>
        /// <param name="propertys"></param>
        /// <returns></returns>
        public virtual IList<ErrorInfo> ValidateInfo(object info, ValidationType type, IList<string> propertys = null)
        {
            var results = new List<ErrorInfo>();
            if (!CheckInfo(info)) return results;
            var valids = GetValidations(info.GetType());
            foreach (var validInfo in valids)
                ValidPorpertyValue(results, info, type, validInfo, propertys);
            return results;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="info"></param>
        /// <param name="valids"></param>
        /// <param name="type"></param>
        /// <param name="propertys"></param>
        /// <returns></returns>
        public virtual IList<ErrorInfo> ValidateInfo(object info, IList<ValidationInfo> valids, ValidationType type, IList<string> propertys = null)
        {
            var results = new List<ErrorInfo>();
            foreach (var validInfo in valids)
                ValidPorpertyValue(results, info, type, validInfo, propertys);
            return results;
        }

        /// <summary>
        /// 得到验证信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual IList<ValidationInfo> GetValidations(string name)
        {
            return !Validations.ContainsKey(name) ? null : Validations[name];
        }

        /// <summary>
        /// 得到验证信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual IList<ValidationInfo> GetValidations(Type type)
        {
            var result = new List<ValidationInfo>();
            while (type != null && type != typeof(object))
            {
                var valids = GetValidations(type.ToString());
                type = type.BaseType;
                if (valids == null) continue;
                foreach (var validationInfo in valids)
                {
                    if (result.Count(it => it.PropertName.Equals(validationInfo.PropertName)) == 0)
                        result.Add(validationInfo);
                }
            }
            return result;
        }

        /// <summary>
        /// 按属性名得到验证信息
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual ValidationInfo GetValidations(string typeName, string propertyName)
        {
            var result = GetValidations(typeName);
            return result?.FirstOrDefault(it => it.PropertName == propertyName);
        }

        /// <summary>
        /// 添加验证信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="infos"></param>
        /// <returns></returns>
        public bool AddValidations(string name, IList<ValidationInfo> infos)
        {
            if (Validations.ContainsKey(name))
                return false;
            Validations.Add(name, infos);
            return true;
        }

        /// <summary>
        /// 得到错误信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propertName"></param>
        /// <returns></returns>
        public virtual ErrorInfo GetErrorInfo(string name, string propertName)
        {
            var info = new ErrorInfo { Key = propertName, Message = propertName };
            if (!Validations.ContainsKey(name)) return info;
            var error = Validations[name].FirstOrDefault(it => it.PropertName.Equals(propertName));
            if (error != null) info.Message = error.Message;
            return info;
        }

        #endregion

        #region 验证方法
        /// <summary>
        /// 是否验证
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual bool CheckInfo(object info)
        {
            if (info != null && Validations.ContainsKey(info.GetType().ToString()) && Validations[info.GetType().ToString()] != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 对属性进行验证
        /// </summary>
        /// <param name="propertys"></param>
        /// <param name="type"></param>
        /// <param name="validInfo"></param>
        /// <param name="results"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual void ValidPorpertyValue(IList<ErrorInfo> results, object info, ValidationType type, ValidationInfo validInfo, IList<string> propertys)
        {
            if (type == ValidationType.Modify && propertys != null && !propertys.Contains(validInfo.PropertName))
                return;
            var rules = GetRules(validInfo.Rules, type);
            FillResults(results, info, validInfo, rules, type, propertys);
        }

        /// <summary>
        /// 填充错误信息
        /// </summary>
        /// <param name="results"></param>
        /// <param name="info"></param>
        /// <param name="validInfo"></param>
        /// <param name="rules"></param>
        /// <param name="type"></param>
        /// <param name="propertys"></param>
        protected virtual void FillResults(IList<ErrorInfo> results, object info, ValidationInfo validInfo, IList<RuleInfo> rules, ValidationType type, IList<string> propertys)
        {
            if (rules == null || rules.Count == 0)
                return;
            object value = null;
            if (type != ValidationType.Add || propertys == null || propertys.Contains(validInfo.PropertName))
            {
                value = GetValue(info, validInfo.PropertName);
            }
            if (type == ValidationType.Add && propertys != null && !propertys.Contains(validInfo.PropertName))
            {
                value = null;
            }
            var regValue = value != null ?
                value is byte[] ? ((byte[])value).LongLength
                : value : null;
            foreach (var rule in rules)
            {
                AddErrorInfo(results, validInfo, regValue, rule);
            }
        }

        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="results"></param>
        /// <param name="validInfo"></param>
        /// <param name="value"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        protected virtual void AddErrorInfo(IList<ErrorInfo> results, ValidationInfo validInfo, object value, RuleInfo rule)
        {
            var isValidate = true;
            if (rule.IsRange)
            {
                object vres = null;
                if (value != null)
                {
                    var reg = new Regex(@"[^\d]");
                    vres = reg.Replace(value.ToString(), "");
                }
                if (vres == null || vres.ToString() == "")
                    vres = TryConvertValue(value, typeof(int)) ?? 0;
                value = vres;
                var values = rule.Pattern.Split('-');
                object startValue = values.Length >= 1 ? values[0] : null;
                object endValue = values.Length >= 2 ? values[1] : null;
                if (startValue != null)
                    isValidate = Convert.ToDouble(value) >= Convert.ToDouble(startValue);
                if (endValue != null)
                    isValidate = isValidate && Convert.ToDouble(value) <= Convert.ToDouble(endValue);
            }
            else
            {
                var revValue = value == null ? "" : value.ToString();
                var option = rule.IsMultiline ? RegexOptions.Multiline : RegexOptions.Singleline;
                option = rule.IsIgnoreCase ? option & RegexOptions.IgnoreCase : option;
                if (!Regex.IsMatch(revValue, rule.Pattern, option))
                {

                    isValidate = false;
                }
            }
            if (!isValidate)
            {
                var message = string.IsNullOrEmpty(rule.Message) ? validInfo.Message : rule.Message;
                var rev = new ErrorInfo { Key = validInfo.PropertName, Message = message };
                results.Add(rev);
            }
        }
        #endregion

        #region 得到需要验证正则表达式
        /// <summary>
        /// 得到需要验证正则表达式
        /// </summary>
        /// <param name="rules"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual IList<RuleInfo> GetRules(IList<RuleInfo> rules, ValidationType type)
        {
            return rules.Where(rule => rule.ValidationTypes != null && rule.ValidationTypes.Contains(type)).ToList();
        }
        /// <summary>
        /// 得到属性值
        /// </summary>
        /// <param name="info"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual object GetValue(object info, string name)
        {
            if (info == null || string.IsNullOrEmpty(name)) return null;
            object rev = name.Contains(".")
                             ? GetRelatePropertyValue(info, name)
                             : GetPropertyValue(info, name);
            if (rev == null) return null;
            return TryConvertValue(rev);
        }

        /// <summary>
        /// 得到属性对象
        /// </summary>
        /// <param name="info"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual object GetPropertyValue(object info, string name)
        {
            var property = info.GetType()
                   .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                   .FirstOrDefault(it => it.Name.Equals(name));
            if (property != null)
            {
                return property.GetValue(info, null);
            }
            return null;
        }
        /// <summary>
        /// 得到关联属性对象
        /// </summary>
        /// <param name="info"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual object GetRelatePropertyValue(object info, string name)
        {
            string[] str = name.Split('.');
            object obj = info;
            foreach (string t in str)
            {
                obj = GetPropertyValue(obj, t);
                if (obj == null)
                    return null;
            }
            return obj;
        }

        /// <summary>
        /// 试着将value转换为type类型的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual object TryConvertValue(object value)
        {
            if (value == null) return null;
            var type = value.GetType();
            if (value.GetType() == typeof(object)) return value;
            try
            {
                return type.IsEnum ? Enum.Parse(type, value.ToString()) : Convert.ChangeType(value, type);
            }
            catch
            {
                return null;
            }
        }

        public virtual object TryConvertValue(object value, Type type)
        {
            if (value == null) return null;
            if (value.GetType() == typeof(object)) return value;
            try
            {
                return type.IsEnum ? Enum.Parse(type, value.ToString()) : Convert.ChangeType(value, type);
            }
            catch
            {
                return null;
            }
        }
        #endregion

    }
}
