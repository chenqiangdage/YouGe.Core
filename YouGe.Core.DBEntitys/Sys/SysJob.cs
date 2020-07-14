using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_job")]
    public class SysJob:BaseModel<int>
    {

        /// <summary>
        /// 任务ID
        /// </summary>
        [Column("job_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        [Column("job_name")]
        public string JobName { get; set; }
        /// <summary>
        /// 任务组名
        /// </summary>
        [Column("job_group")]
        public string JobGroup { get; set; }
        /// <summary>
        /// 调用目标字符串
        /// </summary>
        [Column("invoke_target")]
        public string InvokeTarget { get; set; }
        /// <summary>
        /// cron执行表达式
        /// </summary>
        [Column("cron_expression")]
        public string CronExpression { get; set; }
        /// <summary>
        /// 计划执行错误策略（1立即执行 2执行一次 3放弃执行）
        /// </summary>
        [Column("misfire_policy")]
        public string MisfirePolicy { get; set; }
        /// <summary>
        /// 调用目标字符串
        /// </summary>
        [Column("concurrent")]
        public char ConCurrent { get; set; }
        /// <summary>
        /// 调用目标字符串
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
        public SysJob()
        {
        }
    }
}
