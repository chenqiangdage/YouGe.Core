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
using YouGe.Core.Models.Page;

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
            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                if (ip == "0.0.0.1")
                {
                    ip = httpContextAccessor.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
                }
            }
            
            info.Ip = ip;
            info.RequestTime = DateTime.Now;
            info.RequestType = httpContextAccessor.HttpContext.Request.Method;
            info.RequestUrl = httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            string  UserAgent  =  httpContextAccessor.HttpContext.Request.Headers["User-Agent"];       
            var RequestInfo = new ReqUAInfoCollector(UserAgent).Parse();
            info.Device = RequestInfo.BrowserName + RequestInfo.BrowserVersion;
            info.Os = RequestInfo.SystemName +RequestInfo.SystemVersion;
            return info;
        }

        /**
    * 设置请求分页数据
    */
        protected void startPage()
        {
            PageDomain pageDomain = TableSupport.buildPageRequest();
            int pageNum = pageDomain.getPageNum();
            int pageSize = pageDomain.getPageSize();
            if (StringUtils.isNotNull(pageNum) && StringUtils.isNotNull(pageSize))
            {
                String orderBy = SqlUtil.escapeOrderBySql(pageDomain.getOrderBy());
                PageHelper.startPage(pageNum, pageSize, orderBy);
            }
        }

    }
}
