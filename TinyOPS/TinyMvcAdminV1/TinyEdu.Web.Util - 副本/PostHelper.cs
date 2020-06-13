using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TinyEdu.Web.Util
{
    /// <summary>
    /// 
    /// </summary>
    public static class PostHelper
    {
        public static string AppID = ""; // System.Configuration.ConfigurationManager.AppSettings["AppID"];
        public static string SecretKey = ""; //System.Configuration.ConfigurationManager.AppSettings["SecretKey"];

        public static string GetPostResult(string url, string json)
        {
            string appId = "", transactionId = "", timestamp = "", sign = "";
            string postJson = string.Empty;
            var signdata = string.Empty;

            HttpContent _content;
            if (!string.IsNullOrWhiteSpace(json))
            {
                var _json = (JObject)JsonConvert.DeserializeObject(json);
                _json.AddFirst(new JProperty("sign", HttpContextUtils.Md5Encrypt(json + SecretKey)));
                _json.AddFirst(new JProperty("appid", AppID));
                _content = new StringContent(JsonConvert.SerializeObject(_json), Encoding.UTF8);
                postJson = JsonConvert.SerializeObject(_json);
            }
            else
            {
                var _json = JsonConvert.SerializeObject(
                    new BaseRequest() {
                        appid = AppID,
                        sign = HttpContextUtils.Md5Encrypt("{}" + SecretKey)
                    });
                _content = new StringContent(_json, Encoding.UTF8);
                postJson = _json.ToString();
            }
            _Log4Net.Debug(postJson);
            appId = AppID;
            transactionId = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (string.IsNullOrEmpty(SecretKey))//是否有秘钥
            {
                signdata = appId + timestamp + transactionId + postJson; //需要加密的内容
            }
            else
            {
                signdata = appId + timestamp + transactionId + postJson + SecretKey; //需要加密的内容
            }
            _Log4Net.Debug(signdata);
            sign = HttpContextUtils.Md5Encrypt(signdata);
            _Log4Net.Debug(sign);
            _content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _content.Headers.Add("AppId", AppID);//后台统一分配的APPID	
            _content.Headers.Add("Token", "");//动态令牌	 使用自有的appid和秘钥通过令牌接口获取
            _content.Headers.Add("Timestamp", timestamp);//发送请求的时间，格式"yyyy-MM-dd HH:mm:ss" 例如：2014-07-24 03:07:50
            _content.Headers.Add("TransactionID", transactionId);//消息流水号。yyyyMMddHHmmss+4位顺序号
            _content.Headers.Add("Sign", sign);//规则:大写MD5(AppId+Timestamp+TransactionID+Json+SecrectKey)
            _content.Headers.Add("Version", "1.0");//示例：1.0

            var _client = new HttpClient();
            _client.Timeout = new System.TimeSpan(1, 0, 0, 0, 0);
            var _response = _client.PostAsync(url, _content);
            var _response_result = _response.Result;
            return _response_result.Content.ReadAsStringAsync().Result;
        }
    }
}
