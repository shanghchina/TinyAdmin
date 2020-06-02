using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace OnlyEdu.Common.Dapper.DI
{
    public class XmlIoC : IXmlIoC
    {
        private string _configFile;

        public IDictionary<string, IList<XmlIoCInfo>> XmlIoCs { get; set; } = new Dictionary<string, IList<XmlIoCInfo>>();

        public IDictionary<string, IList<XmlIoCInfo>> GetAll()
        {
            return XmlIoCs;
        }

        public string ConfigFile
        {
            get { return _configFile; }
            set
            {
                _configFile = value;
                LoadConfig();
            }
        }

        public string GetName(string key, string name)
        {
            if (XmlIoCs.ContainsKey(key) && XmlIoCs[key] != null)
            {
                return (from temp in XmlIoCs[key] where temp.Type.Equals(name) select temp.MapTo).FirstOrDefault();
            }
            return null;
        }

        public bool AddNames(string key, IList<XmlIoCInfo> infos)
        {
            if (XmlIoCs.ContainsKey(key))
                return false;
            XmlIoCs.Add(key, infos);
            return true;
        }

        public bool RemoveName(string key)
        {
            if (!XmlIoCs.ContainsKey(key))
                return false;
            XmlIoCs.Remove(key);
            return true;
        }

        public IList<XmlIoCInfo> GetNames(string key)
        {
            if (!XmlIoCs.ContainsKey(key))
                return null;
            return XmlIoCs[key];
        }

        #region 加载配置
        /// <summary>
        /// 加载配置文件
        /// </summary>
        protected virtual void LoadConfig()
        {
            XmlDocument doc = GetXmlDocument();
            LoadIoCByXml(doc);
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
        protected virtual void LoadIoCByXml(XmlDocument doc)
        {
            XmlNodeList nodes = doc.SelectNodes("/containers/container");
            if (nodes == null || nodes.Count == 0)
                return;
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes == null)
                    continue;
                XmlNodeList tempNodes = GetIoCXmlNodes(node);
                AddIoCByXmlNodes(tempNodes, node.Attributes["name"].Value);
            }
        }

        /// <summary>
        /// 得到对象节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual XmlNodeList GetIoCXmlNodes(XmlNode node)
        {
            if (node.Attributes == null)
                return null;
            XmlNodeList nodes = node.SelectNodes("register");
            return nodes;
        }

        /// <summary>
        /// 添加语言信息
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="containerName"></param>
        protected virtual void AddIoCByXmlNodes(XmlNodeList nodes, string containerName)
        {
            if (nodes == null || nodes.Count == 0) return;
            IList<XmlIoCInfo> xmlIoCInfos = new List<XmlIoCInfo>();
            foreach (XmlNode node in nodes)
            {
                var temp = GetValidationInfoByXmlNode(node);
                if (temp != null)
                    xmlIoCInfos.Add(temp);
            }

            if (xmlIoCInfos.Any())
                AddNames(containerName, xmlIoCInfos);
        }

        /// <summary>
        /// 得到验证信息
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual XmlIoCInfo GetValidationInfoByXmlNode(XmlNode node)
        {
            XmlIoCInfo val = null;
            if (node.Attributes != null)
            {
                val = new XmlIoCInfo();
                val.Type = node.Attributes["type"].Value;
                val.MapTo = node.Attributes["mapTo"].Value;
                val.Lifetime = node.Attributes["lifetime"] == null ? "transient" : node.Attributes["lifetime"].Value;
            }
            return val;
        }

        #endregion
    }
}
