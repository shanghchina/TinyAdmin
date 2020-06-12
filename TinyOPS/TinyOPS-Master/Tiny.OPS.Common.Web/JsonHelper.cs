using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Common.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonHelper
    {
        public static JsonResult LargeJson(object data)
        {
            return new JsonResult(data);
        }
        public static JsonResult LargeJson(object data, JsonSerializerSettings serializerSettings)
        {
            return new JsonResult(data, serializerSettings);
        }
    }
}
