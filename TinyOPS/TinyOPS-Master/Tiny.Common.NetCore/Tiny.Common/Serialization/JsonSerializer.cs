using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlyEdu.RLS.Core.Serialization
{
    /// <summary>
    /// Json序列化器。
    /// </summary>
    public sealed class JsonSerializer : ISerializer<string>
    {

        private static JsonSerializerSettings _jsonSettings;

        static JsonSerializer()
        {
            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter();
            datetimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            _jsonSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            _jsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            _jsonSettings.Converters.Add(datetimeConverter);
        }
        #region Implementation of ISerializer<string>

        /// <summary>
        /// 序列化。
        /// </summary>
        /// <param name="instance">需要序列化的对象。</param>
        /// <returns>序列化之后的结果。</returns>
        public string Serialize(object instance)
        {
            try
            {
                if (null == instance)
                    return null;
                return JsonConvert.SerializeObject(instance, Formatting.None, _jsonSettings).Replace("0001-01-01 00:00:00", "");
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 反序列化。
        /// </summary>
        /// <param name="content">序列化的内容。</param>
        /// <param name="type">对象类型。</param>
        /// <returns>一个对象实例。</returns>
        public object Deserialize(string content, Type type)
        {
            return JsonConvert.DeserializeObject(content, type);
        }

        #endregion Implementation of ISerializer<string>
    }
}
