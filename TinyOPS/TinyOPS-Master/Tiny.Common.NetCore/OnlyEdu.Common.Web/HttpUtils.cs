using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlyEdu.Common.Encryption;

namespace OnlyEdu.Common.Web
{
    public class HttpUtils
    {
        public static string Post(string url, string json)
        {
            HttpContent content = new StringContent(json, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var hclient = new HttpClient();
            var hresponse = hclient.PostAsync(url, content).Result;
            var json_result = hresponse.Content.ReadAsStringAsync().Result;
            return json_result;
            //return JsonConvert.DeserializeObject<PostResultParam>(_json_result);
        }
        public static string PostHttps(string url, string json)
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert2, chain, error) =>
            {
                return true;
            };
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string json_result = string.Empty;
            using (var hclient = new HttpClient())
            {
                HttpContent content = new StringContent(json, Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var hresponse = hclient.PostAsync(url, content).Result;
                json_result = hresponse.Content.ReadAsStringAsync().Result;
            }

            return json_result;
            //return JsonConvert.DeserializeObject<PostResultParam>(_json_result);
        }

        public static string PostWithSign(string url, string json, string appid, string secretkey)
        {
            HttpContent content;
            if (!string.IsNullOrWhiteSpace(json))
            {
                var _json = (JObject)JsonConvert.DeserializeObject(json);
                _json.AddFirst(new JProperty("sign", SecurityEncryption.MD5_Encrypt_String(json + secretkey)));
                _json.AddFirst(new JProperty("appid", appid));
                content = new StringContent(JsonConvert.SerializeObject(_json), Encoding.UTF8);
            }
            else
            {
                content = new StringContent(JsonConvert.SerializeObject(new { appid = appid, sign = SecurityEncryption.MD5_Encrypt_String("{}" + secretkey) }), Encoding.UTF8);
            }

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var hclient = new HttpClient();
            hclient.Timeout = new System.TimeSpan(1, 0, 0, 0, 0);
            var hresponse = hclient.PostAsync(url, content);
            var response_result = hresponse.Result;
            return response_result.Content.ReadAsStringAsync().Result;
        }

        public static async Task<string> HttpGet(string url)
        {
            HttpClient httpClient = new HttpClient();
            var data = await httpClient.GetByteArrayAsync(url);
            var result = Encoding.UTF8.GetString(data);
            httpClient.Dispose();
            return result;
        }
    }
}
