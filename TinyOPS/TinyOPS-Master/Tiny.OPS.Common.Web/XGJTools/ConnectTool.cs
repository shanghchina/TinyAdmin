using Newtonsoft.Json;
using Tiny.Common.Web;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tiny.Common.Dapper.DI;

namespace Tiny.OPS.Common.Web.XGJTools
{
    public class ConnectTool
    {
        private static readonly object cacheLock = new object();
        private static readonly object cacheLock2 = new object();
        private static readonly IMemoryCacheManager cache = IoC.Resolve<IMemoryCacheManager>();

        public string GetToken(PushConfig config,string source="")
        {
            object token = null;
            if (config.EnableTokenCache.ToLower() == "true")
            {
                token = cache.Get<object>(config.Value);
            }
            if (token == null)
            {
                lock (cacheLock)
                {
                    if (token == null)
                    {
                        string GetTokenUrl = config.TokenUrl;
                        string appId = config.AppId;
                        string appSecret = config.AppSecret;
                        long t = DateTimeOffset.Now.ToUnixTimeSeconds(); // 相差秒数
                        var key = ToMD5(appId + appSecret + t);
                        var p = JsonConvert.SerializeObject(new { AppID = appId, t = t.ToString(), sign = key });
                        string res = string.Empty;
                        if (source.Contains("api"))
                        {
                            res = PostHttps(GetTokenUrl, p);
                        }
                        else
                        {
                            res = Post(GetTokenUrl, p);
                        }
                        if (res.IndexOf("ErrorCode") < 0)
                        {
                            //成功
                            var info = JsonConvert.DeserializeObject<dynamic>(res);
                            token = info.access_token;
                            if (config.EnableTokenCache.ToLower() == "true")
                            {
                                var expires = Convert.ToInt32(info.expires_in);//token 过期时间
                                var st = DateTime.Now.AddSeconds(expires);
                                cache.Insert(config.Value, token, st);
                            }
                        }
                        else
                        {
                            //错误，记录日志
                            throw new Exception("获取Token出错，错误信息：" + res + "； 请求参数，1_GetTokenUrl：" + GetTokenUrl +" 2_p："+ p+ "；config：" + JsonConvert.SerializeObject(config));
                        }
                    }
                }
            }
            return token.ToString();
        }

        public string GetBasicDataToken(PushConfig config, string source = "")
        {
            object token = null;
            if (config.EnableTokenCache.ToLower() == "true")
            {
                token = cache.Get<object>(config.Value);
            }
            if (token == null)
            {
                lock (cacheLock2)
                {
                    if (token == null)
                    {
                        string GetTokenUrl = config.TokenUrl;
                        string appId = config.AppId;
                        string secret = config.AppSecret;
                        string res = string.Empty;
                        if (source.Contains("api"))
                        {
                            res = HttpGet(GetTokenUrl + "?appId=" + appId + "&secret=" + secret, "").Result;
                        }
                        else
                        {
                            res = Get(GetTokenUrl + "?appId=" + appId + "&secret=" + secret, "");
                        }
                        if (res.IndexOf("ErrorCode") < 0)
                        {
                            //成功
                            var info = JsonConvert.DeserializeObject<dynamic>(res);
                            token = info.access_token;
                            if (config.EnableTokenCache.ToLower() == "true")
                            {
                                var expires = Convert.ToInt32(info.expires_in);//token 过期时间
                                var st = DateTime.Now.AddSeconds(expires);
                                cache.Insert(config.Value, token, st);
                            }
                        }
                        else
                        {
                            //错误，记录日志
                            throw new Exception("获取Token出错，错误信息：" + res);
                        }
                    }
                }
            }
            return token.ToString();
        }
        public static string PostHttps(string url, string json)
        {
            ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)((sender, cert2, chain, error) => true);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string str = string.Empty;
            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent content = (HttpContent)new StringContent(json, Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                str = httpClient.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            }
            return str;
        }
        public static string Post(string url, string postData, string token = "")
        {
            #region webRequeset 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            if (!string.IsNullOrEmpty(token))
                SetHeaderValue(request.Headers, "access_token", token);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            if (postData != null)
            {
                var dataArray = Encoding.UTF8.GetBytes(postData);
                //创建输入流
                Stream dataStream = null;
                try
                {
                    dataStream = request.GetRequestStream();
                    //发送请求
                    dataStream.Write(dataArray, 0, dataArray.Length);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    dataStream.Close();
                }
            }
            //读取返回消息
            var res = string.Empty;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    res = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
            #endregion

        }
        public static async Task<string> HttpGet(string url, string token)
        {
            HttpClient httpClient = new HttpClient();
            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Add("access_token", token);
            byte[] data = await httpClient.GetByteArrayAsync(url);
            string result = Encoding.UTF8.GetString(data);
            httpClient.Dispose();
            return result;
        }
        

        public static string Get(string url, string token)
        {
            WebRequest request = WebRequest.Create(url);
            request.Timeout = (int)new TimeSpan(1, 0, 0, 0, 0).TotalMilliseconds;
            if (!string.IsNullOrEmpty(token))
                SetHeaderValue(request.Headers, "access_token", token);
            request.Method = "GET";
            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                Encoding encode = Encoding.UTF8;
                string resultJson;
                using (StreamReader reader = new StreamReader(stream, encode))
                {
                    resultJson = reader.ReadToEnd();
                    reader.Close();
                }
                stream.Dispose();
                stream.Close();
                response.Close();
                return resultJson;
            }
        }

        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="encypStr"></param>
        /// <returns></returns>
        public static string ToMD5(string encypStr)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            inputBye = Encoding.UTF8.GetBytes(encypStr);

            outputBye = m5.ComputeHash(inputBye);

            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToLower();
            return retStr;
        }
    }
}
