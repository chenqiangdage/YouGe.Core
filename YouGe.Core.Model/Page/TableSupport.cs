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
        public static PageDomain getPageDomain()
        {
            PageDomain pageDomain = new PageDomain();
            pageDomain.PageNum = (ServletUtils.getParameterToInt(PAGE_NUM));
            pageDomain.PageSize = (ServletUtils.getParameterToInt(PAGE_SIZE));
            pageDomain.OrderByColumn = (ServletUtils.getParameter(ORDER_BY_COLUMN));
            pageDomain.IsAsc = (ServletUtils.getParameter(IS_ASC));
            return pageDomain;
        }

        public static PageDomain buildPageRequest()
        {
            return getPageDomain();
        }
        public TableSupport()
        {
        }
    }
}
