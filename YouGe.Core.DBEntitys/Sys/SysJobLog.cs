using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_job_log")]
    public class SysJobLog:BaseModel<int>
    {
        /// <summary>
        /// 任务日志ID
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
        public string Invoke_target { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        [Column("job_message")]
        public string JobMessage { get; set; }
        /// <summary>
        /// 执行状态（0正常 1失败）
        /// </summary>
        [Column("status")]
        public char Status { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        [Column("exception_info")]
        public string ExceptionInfo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public string CreateTime { get; set; }
        public SysJobLog()
        {
        }
    }
}
