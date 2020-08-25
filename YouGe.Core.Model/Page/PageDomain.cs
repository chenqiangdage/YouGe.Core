using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Commons;

namespace YouGe.Core.Models.Page
{
    public class PageDomain
    {
        /// <summary>
        /// 当前记录起始索引
        /// </summary>
        public int? PageNum { get; set; }
        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public string OrderByColumn { get; set; }
        /// <summary>
        /// 排序的方向 "desc" 或者 "asc".
        /// </summary>
        public string IsAsc { get; set; }

        public string getOrderBy()
        {
            if (string.IsNullOrEmpty(OrderByColumn))
            {
                return "";
            }
            return OrderByColumn.toUnderScoreCase() + " " + IsAsc;
        }

       
    }
}
