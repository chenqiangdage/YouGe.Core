using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Commons.Helper
{
    /// <summary>
    /// log4net帮助类
    /// AdoNetAppender仅支持到.net framework4.5，不支持在.net core项目中持久化日志到数据库
    /// </summary>
    public class Log4NetHelper
    {
        // 异常 // 注意：logger name不要写错
        private static readonly ILog logerror = LogManager.GetLogger(Log4NetRepository.loggerRepository.Name, "errLog");
        // 记录
        private static readonly ILog loginfo = LogManager.GetLogger(Log4NetRepository.loggerRepository.Name, "infoLog");
        //告警
        private static readonly ILog logwarn = LogManager.GetLogger(Log4NetRepository.loggerRepository.Name, "warnLog");
        //debug
        private static readonly ILog logdebug = LogManager.GetLogger(Log4NetRepository.loggerRepository.Name, "debugLog");         
        //fatal          
        private static readonly ILog logfatal= LogManager.GetLogger(Log4NetRepository.loggerRepository.Name, "fatalLog");

        public static void Error(string throwMsg, Exception ex)
        {
            string errorMsg = string.Format("【异常描述】：{0} <br>【异常类型】：{1} <br>【异常信息】：{2} <br>【堆栈调用】：{3}",
                new object[] {
                    throwMsg,
                    ex.GetType().Name,
                    ex.Message,
                    ex.StackTrace });
            errorMsg = errorMsg.Replace("\r\n", "<br>");
            logerror.Error(errorMsg);
        }

        public static void Error(string errorMsg)
        {
            logerror.Error(errorMsg);
        }
        public static void Info(string message)
        {
            loginfo.Info(string.Format("【日志信息】：{0}", message));
        }

        public static void Warn(string message)
        {
            logwarn.Warn(message);
        }

        public static void Debugger(string message)
        {
            logdebug.Debug(message);
        }

        public static void Debugger(string message, Exception ex)
        {
            logdebug.Debug(message, ex);
        }
        public static void Fatal(string message)
        {
            logfatal.Fatal(message);
        }

        public static void Fatal(string message, Exception ex)
        {
            logfatal.Fatal(message, ex);
        }

    }
}
