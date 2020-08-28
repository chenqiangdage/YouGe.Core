using Microsoft.AspNetCore.Http;
using System;
namespace YouGe.Core.Models.Page
{
    public class TableSupport
    {
        /// <summary>
        /// 当前记录起始索引
        /// </summary>
        public static readonly string PAGE_NUM = "pageNum";

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public static readonly string PAGE_SIZE = "pageSize";

        /// <summary>
        /// 排序列
        /// </summary>
        public static readonly string ORDER_BY_COLUMN = "orderByColumn";

        /// <summary>
        /// 排序的方向 "desc" 或者 "asc".
        /// </summary>
        public static readonly string IS_ASC = "isAsc";

        /// <summary>
        /// 封装分页对象
        /// </summary>
        /// <returns></returns>
        public static PageDomain getPageDomain(HttpRequest request)
        {
            PageDomain pageDomain = new PageDomain();
            string page = request.Query[PAGE_NUM];
            string pagesize = request.Query[PAGE_SIZE];
            int PageNum, PageSize ;            
            int.TryParse(page, out PageNum);
            int.TryParse(pagesize, out PageSize);
            pageDomain.PageNum = PageNum;
            pageDomain.PageSize = PageSize;
            pageDomain.OrderByColumn = request.Query[ORDER_BY_COLUMN];
            pageDomain.IsAsc = request.Query[IS_ASC];
            return pageDomain;
        }

        public static PageDomain buildPageRequest(HttpRequest request)
        {
            return getPageDomain(request);
        }
        public TableSupport()
        {
        }
    }
}
