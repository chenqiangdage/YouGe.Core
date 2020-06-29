using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Commons.SystemConst
{    
        /// <summary>
        ///  支付状态 
        /// </summary>
        public enum PayStatus {
        /// <summary>
        ///  未支付，
        /// </summary>
        WAITTOPAY =  0,
        /// <summary>
        /// 5下单失败
        /// </summary>
        FAILED = 5,
        /// <summary>
        /// 订单已支付
        /// </summary>
        ORDERPAID = 6,
        /// <summary>
        /// 订单已关闭
        /// </summary>
        ORDERCLOSED = 7,
        /// <summary>
        /// //已支付 
        /// </summary>
        SUCCESS =  10,
        /// <summary>
        /// //退款
        /// </summary>
        REFUND =   11 
        }
    /// <summary>
    /// 支付类型
    /// </summary>
    public enum PayType {

        /// <summary>
        /// native 微信扫一扫
        /// </summary>
        WXNATIVE = 1,
        /// <summary>
        /// 2-jsapi
        /// </summary>
        WXJSAPI = 2,
        /// <summary>
        /// -付款码让商家扫你
        /// </summary>
        WXFUKUANMA = 3,
        /// <summary>
        /// 4-app支付 
        /// </summary>
        WXAPP = 4,
        /// <summary>
        /// h5支付
        /// </summary>
        WXH5 = 5,
        /// <summary>
        /// 小程序支付
        /// </summary>
        WXXCX = 6,
        /// <summary>
        /// 人脸支付
        /// </summary>
        WXFACE = 7,

        /// <summary>
        /// 
        /// 21 当面付-扫码支付
        /// </summary>
        ZFBSM = 21,
        /// <summary>
        /// 22-app支付
        /// </summary>
        ZFBAPP = 22,
        /// <summary>
        /// 23-当面付-二维码/条码/声波支付，别人扫你的
        /// </summary>
        ZFBFUKUANMA = 23,
        /// <summary>
        /// 24-手机网站支付
        /// </summary>
        ZFBJSAPI = 24,
        /// <summary>
        ///  25-电脑网站支付
        /// </summary>
        ZFBPC = 25         
    }

    /// <summary>
    ///   是否通知到对应的系统
    /// </summary>
    public enum NotifyStatus {
      
        /// <summary>
        /// 0 没有
        /// </summary>
        NOTYET = 0,
        /// <summary>
        /// 10已通知
        /// </summary>
        DONE = 10,
        /// <summary>
        /// 11通知N次失败
        /// </summary>
        FAILED = 11,
    }
}
