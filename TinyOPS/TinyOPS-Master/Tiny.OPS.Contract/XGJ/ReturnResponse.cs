using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tiny.OPS.Contract.XGJ
{
   public class ReturnResponse<T>
    {
        [JsonProperty("Data")]
        public ReturnData<T> Data { get; set; }
        [JsonProperty("ErrorCode")]
        public string ErrorCode { get; set; }
        [JsonProperty("ErrorMsg")]
        public string ErrorMsg { get; set; }
    }

    public class ReturnBasicResponse<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }
        [JsonProperty("errcode")]
        public string ErrorCode { get; set; }
        [JsonProperty("errmsg")]
        public string ErrorMsg { get; set; }
    }

    public class ReturnData<T>
    {
        [JsonProperty("List")]
        public List<T> List { get; set; } = new List<T>();
        [JsonProperty("Total")]
        public int Total { get; set; }
    }
}
