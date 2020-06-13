using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Model
{
    /// <summary>
    /// API返回参数
    /// </summary>
    [Serializable]
    public class ResultParam
    {
        public ResultParam()
        {
            IsSuccess = true;
        }

        public ResultParam(string errorMsg)
        {
            AlertMessage = errorMsg;
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { set; get; }
        /// <summary>
        /// 错误说明
        /// </summary>
        public string AlertMessage { set; get; }
        /// <summary>
        /// 返回对象
        /// </summary>
        public object[] ResultInfo { set; get; }
        public int? TotalCount { get; set; }
    }
}
