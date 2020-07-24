using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Gen
{
    [Table("gen_table")]
    public class GenTable:BaseModel<int>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Column("table_id")]        
        public override int Id { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        [Column("table_name")]
        public string TableName { get; set; }
        /// <summary>
        /// 表描述
        /// </summary>
        [Column("table_comment")]
        public string TableComment { get; set; }
        /// <summary>
        /// 实体类名称
        /// </summary>
        [Column("class_name")]
        public string ClassName { get; set; }
        /// <summary>
        /// 使用的模板（crud单表操作 tree树表操作）
        /// </summary>
        [Column("tpl_category")]
        public string TplCategory { get; set; }
        /// <summary>
        /// 生成包路径
        /// </summary>
        [Column("package_name")]
        public string PackageName { get; set; }
        /// <summary>
        /// 生成模块名
        /// </summary>
        [Column("module_name")]
        public string ModuleName { get; set; }
        /// <summary>
        /// 生成业务名
        /// </summary>
        [Column("business_name")]
        public string BusinessName { get; set; }
        /// <summary>
        /// 生成功能名
        /// </summary>
        [Column("function_name")]
        public string FunctionName { get; set; }
        /// <summary>
        /// 生成功能作者
        /// </summary>
        [Column("function_author")]
        public string FunctionAuthor { get; set; }
        /// <summary>
        /// 其它生成选项
        /// </summary>
        [Column("options")]
        public string Options { get; set; }
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

    }
}
