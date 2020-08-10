using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YouGe.Core.Interface.IServices.IManager;
using YouGe.Core.Models.System;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Commons.SystemConst;
using YouGe.Core.Interface.IServices.Sys;
using YouGe.Core.DBEntitys.Sys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using YouGe.Core.Common.Helper;

namespace YouGe.Core.ManagerApi.Controllers
{
    

    /// <summary>
    /// 
    /// </summary>
    public class YouGeController: ControllerBase
    {               
        /// <summary>
        /// 获取远程访问用户的Ip地址
        /// </summary>
        /// <returns>返回Ip地址</returns>
        protected RequestBasicInfo GetRequestInfo(IHttpContextAccessor httpContextAccessor)
        {
            RequestBasicInfo info = new RequestBasicInfo();
            info.Ip=  httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            info.RequestTime = DateTime.Now;
            info.RequestType = httpContextAccessor.HttpContext.Request.Method;
            info.RequestUrl = httpContextAccessor.HttpContext.Request.GetDisplayUrl();
           // info.UserAgent  =  httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            info.UserAgent= new UAParserUserAgent(httpContextAccessor.HttpContext);
            return info;
        }

        
    }
}
