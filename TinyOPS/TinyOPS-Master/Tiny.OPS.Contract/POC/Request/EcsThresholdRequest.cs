using System;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 获取阈值
    /// </summary>
    public class EcsThresholdRequest
    {
        public string appid { get; set; }
        public string sign { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }

}
