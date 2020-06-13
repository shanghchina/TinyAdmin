using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TinyEdu.Web.Util
{
    /// <summary>
    /// 转换大json
    /// </summary>
    public static class JsonHelper
    {
        public static JsonResult LargeJson(object data)
        {
            //return new JsonResult()
            //{
            //    Data = data,
            //    MaxJsonLength = Int32.MaxValue,
            //};
            return new JsonResult(data);
        }
        public static JsonResult LargeJson(object data, JsonSerializerSettings serializerSettings)
        {
            //return new JsonResult()
            //{
            //    Data = data,
            //    JsonRequestBehavior = behavior,
            //    MaxJsonLength = Int32.MaxValue
            //};
            return new JsonResult(data, serializerSettings);
        }
    }
}
