using System;
using System.Collections.Generic;

namespace YouGe.Core.Models.Page
{
    public class TableDataInfo<T>
    {
        private static readonly long serialVersionUID = 1L;

        /// <summary>
        /// 总记录数
        /// </summary>
        public long total { get; set; }

        /// <summary>
        /// 列表数据
        /// </summary>
        public List<T> rows { get; set; }
       // public  object rows { get; set; }
        /// <summary>
        /// 消息状态码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 表格数据对象
        /// </summary>
        public TableDataInfo()
        {
        }

    
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="list">列表数据</param>
        /// <param name="total">总记录数</param>
        public TableDataInfo(List<T> list, int total)
        {
            this.rows = list;
            this.total = total;
        }
 
 
         
    }
}
