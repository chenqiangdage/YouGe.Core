using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_dict_data")]
    public class SysDictData:BaseModel<int>
    {
        /// <summary>
        /// 字典编码
        /// </summary>
        [Column("dict_code")]
        public override int Id { get; set; }
        /// <summary>
        /// 字典排序
        /// </summary>
        [Column("dict_sort")]
        public int DictSort { get; set; }
        /// <summary>
        /// 字典标签
        /// </summary>
        [Column("dict_label")]
        public string DictLabel { get; set; }
        /// <summary>
        /// 字典键值
        /// </summary>
        [Column("dict_value")]
        public string DictValue { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        [Column("dict_type")]
        public string DictType { get; set; }
        /// <summary>
        /// 样式属性（其他样式扩展）
        /// </summary>
        [Column("css_class")]
        public string CssClass { get; set; }
        /// <summary>
        /// 表格回显样式
        /// </summary>
        [Column("list_class")]
        public string ListClass { get; set; }
        /// <summary>
        /// 是否默认（Y是 N否）
        /// </summary>
        [Column("is_default")]
        public char IsDefault { get; set; }
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
        public SysDictData()
        {
        }
    }
}
