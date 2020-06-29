using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Commons.SystemConst
{
    public  class SystemConst
    {
        
    }
    /// <summary>
    /// 费用类型常量
    /// </summary>
    public class ConstFeeType
    {
        /// <summary>
        /// 人民币
        /// </summary>
        public static string CNY = "CNY";
    }

    public class EgateHttpCode
    {
        /// <summary>
        /// 没有网关应用ID
        /// </summary>
        public static string NOGATEID = "404";
        /// <summary>
        /// 没有签名Sign
        /// </summary>

        public static string NOSIGN = "400";
        /// <summary>
        /// 没有指定回调函数
        /// </summary>
        public static string NOCALLBACK = "414";
        /// <summary>
        /// 签名Sign校验失败
        /// </summary>
        public static string SIGNERROR = "444";
    }
}
