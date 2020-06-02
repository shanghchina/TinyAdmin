using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace OnlyEdu.Common
{
    public class BaseApiResult
    {
        /// <summary>
        /// 状态码：0成功，其他状态码各个系统自定义
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
        /// <summary>
        /// 信息：成功，失败内容
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        
    }
}
