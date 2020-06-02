using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Tiny.Common.Dapper.Language
{
    public class Language : ILanguage
    {
        public virtual string ConfigFile { set; get; }
        #region 属性

        private IDictionary<string, IList<LanguageInfo>> _names = new Dictionary<string, IList<LanguageInfo>>();
        /// <summary>
        /// 名称集合
        /// </summary>
        protected IDictionary<string, IList<LanguageInfo>> Names
        {
            get { return _names; }
            set { _names = value; }
        }

        #endregion
        #region 接口的实现

        /// <summary>
        /// 得到名称
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual string GetName(string key, string name)
        {
            if (Names.ContainsKey(key) && Names[key] != null)
            {
                return (from language in Names[key] where language.Name.Equals(name) select language.Message).FirstOrDefault();
            }
            return null;
        }
        /// <summary>
        /// 添加名称
        /// </summary>
        /// <param name="key"></param>
        /// <param name="infos"></param>
        /// <returns></returns>
        public virtual bool AddNames(string key, IList<LanguageInfo> infos)
        {
            if (Names.ContainsKey(key))
                return false;
            var type = Type.GetType(key);
            infos.ToList().ForEach(item =>
            {
                if (type != null && type.IsEnum)
                    item.Value = (int)Enum.Parse(type, item.Name);
            });
            Names.Add(key, infos);
            return true;
        }



        /// <summary>
        /// 移除名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool RemoveName(string key)
        {
            if (!Names.ContainsKey(key))
                return false;
            Names.Remove(key);
            return true;
        }

        public virtual IList<LanguageInfo> GetNames(string key)
        {
            if (!Names.ContainsKey(key))
                return null;
            return Names[key];
        }



        #endregion
    }
}
