using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_dict_type")]
    public class SysDictType:BaseModel<int>
    {
        /// <summary>
        /// 字典编码
        /// </summary>
        [Column("dict_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        [Column("dict_name")]
        public string DictName { get; set; }


        /// <summary>
        /// 字典类型
        /// </summary>
        [Column("dict_type")]
        public string DictType { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
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

        public SysDictType()
        {
        }
    }
}
