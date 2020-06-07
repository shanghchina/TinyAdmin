using Newtonsoft.Json;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// Token 验证请求
    /// </summary>
    public class TokenVerifyRequest
    {
        /// <summary>
        /// 客户端密钥
        /// </summary>
        [JsonProperty(PropertyName = "secret")]
        public string Secret { get; set; }

        /// <summary>
        /// 应用标识
        /// </summary>
        [JsonProperty(PropertyName = "AppId")]
        public string ClientAppId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
    }
}
