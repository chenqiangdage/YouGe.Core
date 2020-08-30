using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_config")]
    public class SysConfig :BaseModel<int>
    {
        /// <summary>
        /// 参数主键
        /// </summary>
        [Column("config_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        [Column("config_name")]
        public string ConfigName { get; set; }
        /// <summary>
        /// 参数键值
        /// </summary>
        [Column("config_key")]
        public string ConfigKey { get; set; }
        /// <summary>
        /// 系统内置（Y是 N否）
        /// </summary>
        [Column("config_value")]
        public string ConfigValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("config_type")]
        public string ConfigType { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime? CreateTime { get; set; }
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
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; }
        [NotMapped]
        public DateTime? beginTime { get; set; }
        [NotMapped]
        public DateTime? endTime { get; set; }

        public SysConfig()
        {
        }
    }
}
