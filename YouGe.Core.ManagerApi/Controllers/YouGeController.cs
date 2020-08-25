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
using YouGe.Core.Common.SystemConst;

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

        /// <summary>
        /// 设置请求分页数据
        /// </summary>
        protected void startPage(IHttpContextAccessor httpContextAccessor)
        {
            //to do 这里用的是mybatis的 pagehelper插件
            //net core 并没有这个插件，所以要做自己的分页
            //这里就是拿请求中的分页参数

            PageDomain pageDomain = TableSupport.buildPageRequest();
           // int? pageNum = pageDomain.PageNum;
           // int? pageSize = pageDomain.PageSize;
           // if ( pageNum.HasValue && pageSize.HasValue)
           // {
           //     string orderBy = SqlUtil.escapeOrderBySql(pageDomain.getOrderBy());
           //     PageHelper.startPage(pageNum, pageSize, orderBy);
           // }
        }
        /// <summary>
        /// 响应请求分页数据
        /// </summary>
       // @SuppressWarnings({ "rawtypes", "unchecked" })
        protected TableDataInfo<T> getDataTable<T>(List<T> list,long total)
        {
            //to do 这个方式可能不好，全部移植完毕后优化这个分页的
            TableDataInfo<T> rspData = new TableDataInfo<T>();
            rspData.code = HttpStatusConst.SUCCESS;
            rspData.msg = "查询成功";
            rspData.rows =list;
            rspData.total = total;
            return rspData;
        }

    }
}
