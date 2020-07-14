using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_oper_log")]
    public class SysOperLog:BaseModel<int>
    {
        /// <summary>
        /// 访问ID
        /// </summary>
        [Column("oper_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 模块标题
        /// </summary>
        [Column("title")]
        public string Title { get; set; }
        /// <summary>
        /// 业务类型（0其它 1新增 2修改 3删除）
        /// </summary>
        [Column("business_type")]
        public int BusinessType { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        [Column("method")]
        public string Method { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        [Column("request_method")]
        public string RequestMethod { get; set; }
        /// <summary>
        /// 操作类别（0其它 1后台用户 2手机端用户）
        /// </summary>
        [Column("operator_type")]
        public int OperatorType { get; set; }
        /// <summary>
        /// 操作人员        /// </summary>
        [Column("oper_name")]
        public string OperName { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Column("dept_name")]
        public string DeptName { get; set; }
        /// <summary>
        /// 请求URL
        /// </summary>
        [Column("oper_url")]
        public string Oper_url { get; set; }
        /// <summary>
        /// 主机地址
        /// </summary>
        [Column("oper_ip")]
        public string OperIp { get; set; }
        /// <summary>
        /// 操作地点
        /// </summary>
        [Column("oper_location")]
        public string OperLocation { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        [Column("oper_param")]
        public string OperParam { get; set; }
        /// <summary>
        /// 返回参数
        /// </summary>
        [Column("json_result")]
        public string JsonResult { get; set; }
        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        [Column("status")]
        public int Status { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        [Column("error_msg")]
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        [Column("oper_time")]
        public DateTime? OperTime { get; set; }
        public SysOperLog()
        {
        }
    }
}
