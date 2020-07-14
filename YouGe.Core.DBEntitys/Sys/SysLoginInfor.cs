using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_logininfor")]
    public class SysLoginInfor:BaseModel<int>
    {
        /// <summary>
        /// 访问ID
        /// </summary>
        [Column("info_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        [Column("user_name")]
        public string UserName { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        [Column("ipaddr")]
        public string ipaddr { get; set; }
        /// <summary>
        /// 登录地点
        /// </summary>
        [Column("login_location")]
        public string LoginLocation { get; set; }
        /// <summary>
        /// 浏览器类型
        /// </summary>
        [Column("browser")]
        public string Browser { get; set; }
        /// <summary>
        /// 操作系统
        /// </summary>
        [Column("os")]
        public string Os { get; set; }
        /// <summary>
        /// 登录状态（0成功 1失败）
        /// </summary>
        [Column("status")]
        public char status { get; set; }
        /// <summary>
        /// 提示消息
        /// </summary>
        [Column("msg")]
        public string msg { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        [Column("login_time")]
        public DateTime? LoginTime { get; set; }
        public SysLoginInfor()
        {
        }
    }
}
