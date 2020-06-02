using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Common
{
    public static class _Log4Net
    {
        private static ILoggerRepository loggerRepository;

        public static ILoggerRepository LoggerRepository { get; private set; }
        public static ILog Log { get; private set; }

        /// <summary>
        /// 静态构造函数，程序启动时自动执行
        /// </summary>
        //static Logger()
        //{
        //    LoggerRepository = CreateLoggerRepository();
        //    LoadLog4NetConfig();
        //}

        /// <summary>
        /// 初始化日志
        /// </summary>
        /// <returns></returns>
        public static void LoadLogger()
        {
            LoggerRepository = CreateLoggerRepository();
            LoadLog4NetConfig();
        }

        /// <summary>
        /// 创建日志仓储实例
        /// </summary>
        /// <returns></returns>
        private static ILoggerRepository CreateLoggerRepository()
        {
            loggerRepository = loggerRepository ?? LogManager.CreateRepository("GlobalExceptionHandler"); // 单例
            return loggerRepository;
        }

        /// <summary>
        /// 加载log4net配置
        /// </summary>
        private static void LoadLog4NetConfig()
        {
            // 配置log4net
            string log4netConfigPath = System.IO.Directory.GetCurrentDirectory() + "/log4net.config";
            log4net.Config.XmlConfigurator.Configure(loggerRepository, new System.IO.FileInfo(log4netConfigPath));

            // 创建log实例
            string logName = AppDomain.CurrentDomain.FriendlyName;
            Log = LogManager.GetLogger(loggerRepository.Name, logName);

            Log.Info("已加载日志配置");
        }


        /// <summary>
        /// desc：处理日志信息记录
        /// </summary>
        /// <param name="message">消息</param>
        public static void Info(string message)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(loggerRepository.Name, "loginfo");
            logger.Info(message);
        }

        /// <summary>
        /// 日志信息记录，可以记录多个参数
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="objects">参数内容</param>
        public static void InfoFormat(string message, params object[] objects)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(loggerRepository.Name, "loginfo");
            logger.InfoFormat(message, objects);
        }

        /// <summary>
        /// desc：出错信息记录
        /// </summary>
        /// <param name="message">消息</param>
        public static void Error(string message)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(loggerRepository.Name, "logerror");
            logger.Error(message);
        }

        /// <summary>
        /// 出错信息记录
        /// </summary>
        /// <param name="message">方法等信息描述</param>
        /// <param name="ex">异常对象</param>
        public static void Error(string message, Exception ex)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(loggerRepository.Name, "logerror");
            logger.Error(message, ex);
        }

        /// <summary>
        /// 出错信息记录，可以记录多个参数
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="objects">参数内容</param>
        public static void ErrorFormat(string message, params object[] objects)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(loggerRepository.Name, "logerror");
            logger.ErrorFormat(message, objects);
        }

        /// <summary>
        /// Debug信息记录
        /// </summary>
        /// <param name="message">消息内容</param>
        public static void Debug(string message)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(loggerRepository.Name, "debuginfo");
            logger.Debug(message);
        }
        /// <summary>
        /// Debug信息记录，可以记录多个参数
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="objects">参数内容</param>
        public static void DebugFormat(string message, params object[] objects)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(loggerRepository.Name, "debuginfo");
            logger.DebugFormat(message, objects);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(loggerRepository.Name, "warninfo");
            logger.Warn(message);
        }

        /// <summary>
        /// 警告信息，可以记录多个参数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="objects"></param>
        public static void WarningFormat(string message, params object[] objects)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(loggerRepository.Name, "warninfo");
            logger.WarnFormat(message, objects);
        }
    }
}