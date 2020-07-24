using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace YouGe.Core.DBEntitys.Gen
{

    [Table("gen_table_column")]
    public class GenTableColumn:BaseModel<int>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Column("column_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 归属表编号
        /// </summary>
        [Column("table_id")]
        public string TableId { get; set; }
        /// <summary>
        /// 列名称
        /// </summary>
        [Column("column_name")]
        public string ColumnName { get; set; }
        /// <summary>
        /// 列描述
        /// </summary>
        [Column("column_comment")]
        public string ColumnComment { get; set; }
        /// <summary>
        /// 列类型
        /// </summary>
        [Column("column_type")]
        public string ColumnType { get; set; }
        /// <summary>
        /// JAVA类型
        /// </summary>
        [Column("java_type")]
        public string JavaType { get; set; }
        /// <summary>
        /// JAVA字段名
        /// </summary>
        [Column("java_field")]
        public string JavaField { get; set; }
        /// <summary>
        /// 是否主键（1是）
        /// </summary>
        [Column("is_pk")]
        public char IsPk { get; set; }
        /// <summary>
        /// 是否自增（1是）
        /// </summary>
        [Column("is_increment")]
        public char IsIncrement { get; set; }
        /// <summary>
        /// 是否必填（1是）
        /// </summary>
        [Column("is_required")]
        public char IsRequired { get; set; }
        /// <summary>
        /// 是否为插入字段（1是）
        /// </summary>
        [Column("is_insert")]
        public char IsInsert { get; set; }
        /// <summary>
        /// 是否编辑字段（1是）
        /// </summary>
        [Column("is_edit")]
        public char IsEdit { get; set; }
        /// <summary>
        /// 是否列表字段（1是）
        /// </summary>
        [Column("is_list")]
        public char IsList { get; set; }
        /// <summary>
        /// 是否查询字段（1是）
        /// </summary>
        [Column("is_query")]
        public char IsQuery { get; set; }
        /// <summary>
        /// 查询方式（等于、不等于、大于、小于、范围）
        /// </summary>
        [Column("query_type")]
        public string QueryType { get; set; }
        /// <summary>
        /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件
        /// </summary>
        [Column("html_type")]
        public string HtmlType { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        [Column("dict_type")]
        public string DictType { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("sort")]
        public int Sort { get; set; }        
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

    }
}
