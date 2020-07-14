using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_post")]
    public class SysPost:BaseModel<int>
    {
        /// <summary>
        /// 岗位ID
        /// </summary>
        [Column("post_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 岗位编码
        /// </summary>
        [Column("post_code")]
        public string PostCode { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        [Column("post_name")]
        public string PostName { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        [Column("post_sort")]
        public string PostSort { get; set; }
        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Column("status")]
        public char status { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新者
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime? UpdateTime { get; set; }
        [Column("remark")]
        public string Remark { get; set; }
        public SysPost()
        {
        }
    }
}
