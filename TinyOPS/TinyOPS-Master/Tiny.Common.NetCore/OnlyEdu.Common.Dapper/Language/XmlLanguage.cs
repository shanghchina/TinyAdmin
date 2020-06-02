using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace OnlyEdu.Common.Dapper.Language
{
    public class XmlLanguage : Language
    {
        #region 属性
        private string _configFile;
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public override string ConfigFile
        {
            get { return _configFile; }
            set
            {
                _configFile = value;
                LoadConfig();
            }

        }
        #endregion

        #region 加载配置
        /// <summary>
        /// 加载配置文件
        /// </summary>
        protected virtual void LoadConfig()
        {
            XmlDocument doc = GetXmlDocument();
            LoadLanguageByXml(doc);
        }
        /// <summary>
        /// 得到XmlDocument
        /// </summary>
        /// <returns></returns>
        protected virtual XmlDocument GetXmlDocument()
        {
            string filename = System.IO.Path.Combine(Path.GetDirectoryName(ConfigFile), ConfigFile);
            return GetXmlDocument(filename);
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

        #region 加载语言信息

        /// <summary>
        /// 加载语言信息
        /// </summary>
        /// <param name="doc"></param>
        protected virtual void LoadLanguageByXml(XmlDocument doc)
        {
            XmlNodeList nodes = doc.SelectNodes("/configuration/Dislan/XmlLanguage/Language");
            AddLanguageByXmlNodes(nodes);
        }

        /// <summary>
        /// 得到对象节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual XmlNodeList GetLanguageXmlNodes(XmlNode node)
        {
            if (node.Attributes == null)
                return null;
            string fileName = System.IO.Path.Combine(Path.GetDirectoryName(ConfigFile),
                                                     node.Attributes["Path"].Value);
            XmlDocument doc = GetXmlDocument(fileName);
            XmlNodeList nodes = doc.SelectNodes("/configuration/Dislan/XmlLanguage/Language");
            return nodes;
        }

        /// <summary>
        /// 添加语言信息
        /// </summary>
        /// <param name="nodes"></param>
        protected virtual void AddLanguageByXmlNodes(XmlNodeList nodes)
        {
            if (nodes == null || nodes.Count == 0) return;
            foreach (XmlNode node in nodes)
            {
                FillNamesByXmlNode(node);
            }
        }
        /// <summary>
        /// 填充名称
        /// </summary>
        /// <param name="node"></param>
        protected virtual void FillNamesByXmlNode(XmlNode node)
        {
            var nodes = node.SelectNodes("Message");
            if (nodes == null || nodes.Count == 0) return;
            if (node.Attributes == null) return;
            var languages = (from XmlNode nd in nodes select GetLanguageInfoByXmlNode(nd)).ToList();
            AddNames(node.Attributes["Name"].Value, languages);
        }

        /// <summary>
        /// 添加名称
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual LanguageInfo GetLanguageInfoByXmlNode(XmlNode node)
        {
            var info = new LanguageInfo();
            if (node.Attributes != null)
            {
                info.Name = node.Attributes["Name"].Value;
                info.Message = node.Attributes["Value"].Value;
                info.Value = node.Attributes["Id"] == null ? 0 : Convert.ToInt32(node.Attributes["Id"].Value);
            }
            return info;
        }

        #endregion
    }
}
