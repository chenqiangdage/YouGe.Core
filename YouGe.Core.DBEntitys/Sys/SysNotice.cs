using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_notice")]
    public class SysNotice:BaseModel<int>
    {
        /// <summary>
        /// 公告ID
        /// </summary>
        [Column("notice_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 公告标题
        /// </summary>
        [Column("notice_title")]
        public string NoticeTitle { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        [Column("notice_content")]
        public string NoticeContent { get; set; }
        /// <summary>
        /// 公告类型（1通知 2公告）
        /// </summary>
        [Column("notice_type")]
        public char NoticeType { get; set; }
        /// <summary>
        /// 公告状态（0正常 1关闭）
        /// </summary>
        [Column("status")]
        public char Status { get; set; }

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
        public SysNotice()
        {
        }
    }
}
