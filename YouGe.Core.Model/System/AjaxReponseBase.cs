using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Common.SystemConst;
using YouGe.Core.Commons;
using YouGe.Core.Commons.SystemConst;

namespace YouGe.Core.Models.System
{
    public class AjaxReponseBase :Dictionary<string,object>
    {



        /// <summary>
        /// 提示消息
        /// </summary>
        public static readonly string  MSG_TAG = "msg";
        /// <summary>
        /// 消息代码
        /// </summary>
        public static  readonly string CODE_TAG ="code";
        /// <summary>
        /// 数据对象
        /// </summary>
        public static readonly string DATA_TAG = "data";

        public AjaxReponseBase()
        { 
        }
        /// <summary>
        /// 初始化一个新创建的 AjaxReponseBase 对象
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public AjaxReponseBase(int code, string msg) {
            this.Add(CODE_TAG, code);
            this.Add(MSG_TAG, msg);
        }
        /// <summary>
        ///  初始化一个新创建的 AjaxReponseBase 对象
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public AjaxReponseBase(int code, string msg,object data )
        {
            this.Add(CODE_TAG, code);
            this.Add(MSG_TAG, msg);
            if (data != null)
            {
                this.Add(DATA_TAG, data);
            }            
        }


        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <returns></returns>
        public static AjaxReponseBase Success(object data)
        {
            return AjaxReponseBase.Success(YouGeSystemConst.OPERATE_SUCESS, data);
        }

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <returns></returns>
        public static AjaxReponseBase Success()
        {
            return AjaxReponseBase.Success(YouGeSystemConst.OPERATE_SUCESS);
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static AjaxReponseBase Success(string msg) {
            return AjaxReponseBase.Success(msg, null);
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static AjaxReponseBase Success(string msg,object data)
        {
            return new AjaxReponseBase(HttpStatusConst.SUCCESS, msg, data);
        }
        /// <summary>
        /// 返回错误消息
        /// </summary>
        /// <returns></returns>
        public static AjaxReponseBase Error()
        {
            return AjaxReponseBase.Error(YouGeSystemConst.OPERATE_FAIL);
        }

        /// <summary>
        /// 返回错误消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static AjaxReponseBase Error(string msg)
        {
            return AjaxReponseBase.Error(msg, null);
        }
        /// <summary>
        /// 返回错误消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static AjaxReponseBase Error(string msg, object data)
        {
            return new AjaxReponseBase(HttpStatusConst.ERROR, msg, data);   
        }
    }
}
