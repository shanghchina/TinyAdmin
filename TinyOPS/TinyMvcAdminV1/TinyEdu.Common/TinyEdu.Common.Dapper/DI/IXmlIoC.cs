using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Common.Dapper.DI
{
    public interface IXmlIoC
    {
        /// <summary>
        /// 得到名称
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetName(string key, string name);

        /// <summary>
        /// 添加名称
        /// </summary>
        /// <param name="key"></param>
        /// <param name="infos"></param>
        /// <returns></returns>
        bool AddNames(string key, IList<XmlIoCInfo> infos);
        /// <summary>
        /// 移除名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool RemoveName(string key);
        /// <summary>
        /// 得到包含名称为name的所有集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IList<XmlIoCInfo> GetNames(string key);
        /// <summary>
        /// 获取所有集合
        /// </summary>
        /// <returns></returns>
        IDictionary<string, IList<XmlIoCInfo>> GetAll();
        /// <summary>
        /// 配置文件
        /// </summary>
        string ConfigFile { set; get; }
    }
}
