﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace TinyEdu.Common.Dapper.Validate
{
    public class XmlValidation : Validation
    {
        private string _configFile;
        public override string ConfigFile
        {
            get { return _configFile; }
            set
            {
                _configFile = value;
                LoadConfig();
            }
        }

        #region 加载配置
        /// <summary>
        /// 加载配置文件
        /// </summary>
        protected virtual void LoadConfig()
        {
            IDictionary<string, RuleInfo> rules = GetRuleByXml(System.IO.Path.Combine(Path.GetDirectoryName(ConfigFile), "Rule.xml"));
            LoadValidationByXml(ConfigFile, rules);
        }

        /// <summary>
        ///  得到XmlDocument
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected virtual XmlDocument GetXmlDocument(string fileName)
        {
            var doc = new XmlDocument();
            doc.Load(fileName);
            return doc;
        }
        #endregion

        #region 得到规则

        /// <summary>
        /// 得到规制
        /// </summary>
        /// <param name="doc"></param>
        protected virtual IDictionary<string, RuleInfo> GetRuleByXml(string rulePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(rulePath);
            IDictionary<string, RuleInfo> rules = new Dictionary<string, RuleInfo>();
            XmlNodeList xnPaths = doc.SelectNodes("/configuration/Filter/XmlValidation/RulePath");
            if (xnPaths == null || xnPaths.Count == 0)
                return rules;
            foreach (XmlNode node in xnPaths)
            {
                XmlNodeList nodes = GetRuleXmlNodes(node);
                AddRuleByXmlNodes(rules, nodes);
            }
            return rules;
        }

        /// <summary>
        /// 得到规则节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual XmlNodeList GetRuleXmlNodes(XmlNode node)
        {
            if (node.Attributes == null)
                return null;
            XmlNodeList nodes = node.SelectNodes("/configuration/Filter/XmlValidation/Rule");
            return nodes;
        }

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="rules"></param>
        /// <param name="nodes"></param>
        protected virtual void AddRuleByXmlNodes(IDictionary<string, RuleInfo> rules, XmlNodeList nodes)
        {
            if (nodes == null || nodes.Count == 0)
                return;
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes != null && node.Attributes["Regular"] != null)
                {
                    rules.Add(node.Attributes["Name"].Value,
                              new RuleInfo
                              {
                                  Pattern = node.Attributes["Regular"].Value,
                                  IsRange =
                                          node.Attributes["IsRange"] != null &&
                                          "true".Equals(node.Attributes["IsRange"].Value)
                              });
                }
            }
        }
        #endregion

        #region 加载验证信息
        /// <summary>
        /// 加载对象验证信息
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="rules"></param>
        protected virtual void LoadValidationByXml(string file, IDictionary<string, RuleInfo> rules)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNodeList xnPaths = doc.SelectNodes("/configuration/Filter/XmlValidation/Model");
            if (xnPaths == null || xnPaths.Count == 0)
                return;
            foreach (XmlNode node in xnPaths)
            {
                XmlNodeList nodes = GetValidationXmlNodes(node);
                AddValidationByXmlNodes(nodes, rules);
            }
        }

        /// <summary>
        /// 得到对象节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual XmlNodeList GetValidationXmlNodes(XmlNode node)
        {
            if (node.Attributes == null)
                return null;
            XmlNodeList nodes = node.SelectNodes("/configuration/Filter/XmlValidation/Model");
            return nodes;
        }

        /// <summary>
        /// 添加验证信息
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="rules"></param>
        protected virtual void AddValidationByXmlNodes(XmlNodeList nodes, IDictionary<string, RuleInfo> rules)
        {
            if (nodes == null || nodes.Count == 0)
                return;
            foreach (XmlNode node in nodes)
            {
                IList<ValidationInfo> valids = GetValidtionsByXmlNode(node, rules);
                if (node.Attributes != null) AddValidations(node.Attributes["Name"].Value, valids);
            }
        }

        /// <summary>
        /// 得到属性验证信息
        /// </summary>
        /// <param name="node"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        protected virtual IList<ValidationInfo> GetValidtionsByXmlNode(XmlNode node, IDictionary<string, RuleInfo> rules)
        {
            IList<ValidationInfo> valids = new List<ValidationInfo>();
            XmlNodeList nodes = node.SelectNodes("Property");
            AddValidationPropertyByXmlNode(nodes, valids, rules);
            return valids;
        }
        /// <summary>
        /// 添加验证属性
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="valids"></param>
        /// <param name="rules"></param>
        protected virtual void AddValidationPropertyByXmlNode(XmlNodeList nodes, IList<ValidationInfo> valids, IDictionary<string, RuleInfo> rules)
        {
            if (nodes == null || nodes.Count == 0)
                return;
            foreach (XmlNode node in nodes)
            {
                valids.Add(GetValidationInfoByXmlNode(node, rules));
            }
        }

        /// <summary>
        /// 得到验证信息
        /// </summary>
        /// <param name="node"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        protected virtual ValidationInfo GetValidationInfoByXmlNode(XmlNode node, IDictionary<string, RuleInfo> rules)
        {
            var val = new ValidationInfo();
            if (node.Attributes != null)
            {
                val.PropertName = node.Attributes["PropertyName"].Value;
                val.Message = node.Attributes["Message"] == null ? string.Empty : node.Attributes["Message"].Value;
            }
            val.Rules = GetPropertyRulesByXmlNode(node, rules);
            return val;
        }

        /// <summary>
        /// 得到属性验证规则
        /// </summary>
        /// <param name="node"></param>
        /// <param name="rules"></param>
        protected virtual IList<RuleInfo> GetPropertyRulesByXmlNode(XmlNode node, IDictionary<string, RuleInfo> rules)
        {
            var propertyRules = new List<RuleInfo>();
            XmlNodeList nodes = node.SelectNodes("Validation");
            if (nodes == null || nodes.Count == 0) return propertyRules;
            FillPropertyRuleByXmlNodes(nodes, propertyRules, rules);
            return propertyRules;
        }

        /// <summary>
        /// 填充验证类型
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="propertyRules"></param>
        /// <param name="rules"></param>
        protected virtual void FillPropertyRuleByXmlNodes(XmlNodeList nodes, IList<RuleInfo> propertyRules, IDictionary<string, RuleInfo> rules)
        {
            foreach (XmlNode node in nodes)
            {
                AddPropertyRulesByXmlNode(node, propertyRules, rules);
            }
        }

        /// <summary>
        /// 添加验证类型
        /// </summary>
        /// <param name="node"></param>
        /// <param name="propertyRules"></param>
        /// <param name="rules"></param>
        protected virtual void AddPropertyRulesByXmlNode(XmlNode node, IList<RuleInfo> propertyRules,
                                                         IDictionary<string, RuleInfo> rules)
        {
            if (node.Attributes == null || !rules.ContainsKey(node.Attributes["RuleName"].Value))
                return;
            var rule = new RuleInfo();
            var args = GetPropertyRuleArgsByXmlNode(node);
            rule.ValidationTypes = node.Attributes["ValidationType"] != null
                                       ? GetValidationTypesByString(node.Attributes["ValidationType"].Value)
                                       : GetDefaultValidationTypes();
            rule.Pattern = ReplacePatternArgs(rules[node.Attributes["RuleName"].Value].Pattern, args);
            rule.IsRange = rules[node.Attributes["RuleName"].Value].IsRange;
            rule.IsMultiline = node.Attributes["IsMultiline"] != null &&
                               "true".Equals(node.Attributes["IsMultiline"].Value);
            rule.IsIgnoreCase = node.Attributes["IsIgnoreCase"] != null &&
                                "true".Equals(node.Attributes["IsIgnoreCase"].Value);
            rule.Message = node.Attributes["Message"] != null ? node.Attributes["Message"].Value : null;
            propertyRules.Add(rule);
        }

        /// <summary>
        /// 替换正则表达式里的参数
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual string ReplacePatternArgs(string rule, IList<string> args)
        {
            for (int i = 0; i < args.Count; i++)
            {
                rule = rule.Replace(string.Format("P{0}", i), args[i]);
            }
            return rule;
        }

        /// <summary>
        /// 得到默认验证类型
        /// </summary>
        /// <returns></returns>
        protected virtual IList<ValidationType> GetDefaultValidationTypes()
        {
            IList<ValidationType> types = new List<ValidationType>();
            types.Add(ValidationType.Add);
            types.Add(ValidationType.Modify);
            return types;
        }
        /// <summary>
        /// 根据节点配置得到验证类型
        /// </summary>
        /// <param name="typeString"></param>
        /// <returns></returns>
        protected virtual IList<ValidationType> GetValidationTypesByString(string typeString)
        {
            IList<ValidationType> types = new List<ValidationType>();
            if (string.IsNullOrEmpty(typeString)) return types;
            string[] values = typeString.Split('|');
            foreach (string value in values)
            {
                types.Add((ValidationType)Enum.Parse(typeof(ValidationType), value));
            }
            return types;
        }
        /// <summary>
        /// 得到规则参数
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual IList<string> GetPropertyRuleArgsByXmlNode(XmlNode node)
        {
            IList<string> values = new List<string>();
            for (int i = 0; ; i++)
            {
                string pName = string.Format("P{0}", i);
                if (node.Attributes != null && node.Attributes[pName] != null)
                    values.Add(node.Attributes[pName].Value);
                else
                    break;
            }
            return values;
        }
        #endregion
    }
}
