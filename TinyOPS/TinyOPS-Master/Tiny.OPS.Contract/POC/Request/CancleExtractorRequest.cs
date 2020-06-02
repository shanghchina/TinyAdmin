using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 取消控制器请求信息
    /// </summary>
   public class CancleExtractorRequest
    {
        public List<Guid> ExtractorGuids { get; set; }
    }
}
