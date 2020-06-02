using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 响应编码
    /// </summary>
    public class CodeConst
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const string Success = "0000";

        /// <summary>
        /// 失败
        /// </summary>
        public const string Failed = "9999";

        /// <summary>
        /// 参数有误
        /// </summary>
        public const string ParamterError = "0001";

        /// <summary>
        /// 业务逻辑错误
        /// </summary>
        public const string BusinessError = "0002";

        /// <summary>
        /// 会话过期
        /// </summary>
        public const string SessionExpiration = "0003";

        /// <summary>
        /// 危险数据
        /// </summary>
        public const string DangerParamter = "0004";

        /// <summary>
        /// 系统异常
        /// </summary>
        public const string SystemException = "0005";
    }
}
