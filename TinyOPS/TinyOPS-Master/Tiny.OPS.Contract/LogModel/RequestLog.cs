using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 请求日志数据模型
    /// </summary>
    public class RequestLog
    {
        /// <summary>
        /// 响应时间yyyyMMddHHmmss
        /// </summary>
        public string LogTime => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 统一应用域ID
        /// </summary>
        public string DomainTypeId => "POCAPI-123";

        /// <summary>
        /// 应用名称
        /// </summary>
        public string ApplicationName => "POCAPI";

        /// <summary>
        /// 请求头
        /// </summary>
        public HeadModel Head { get; set; }

        /// <summary>
        /// 日志记录所在命名空间明细
        /// </summary>
        public string Ojbect => "Tiny.OPS.WebApi";

        /// <summary>
        /// 日志记录所在方法
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 日志级别(1000.Fatal 2000.Error 3000.Warn 4000.Info 5000.Debug)
        /// </summary>
        public string Loglevel => "Info";
        /// <summary>
        /// 请求用户IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string URI { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogData { get; set; }
    }

    /// <summary>
    /// 请求头模型
    /// </summary>
    public class HeadModel
    {
        /// <summary>
        /// 后台统一分配的APPID	
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 请求流水号
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// 动态令牌,使用自有的appid和秘钥通过令牌接口获取
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 发送请求的时间，格式"yyyy-MM-dd HH:mm:ss" 例如：2014-07-24 03:07:50
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// 签名大写MD5(AppId+Timestamp+TransactionID+Json+SecrectKey)
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
    }
}
