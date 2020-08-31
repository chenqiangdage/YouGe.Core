using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_dept")]
    public class SysDept : BaseModel<int>
    {                      
            /// <summary>
            /// 部门id
            /// </summary>
            [Column("dept_id")]
            public override int Id { get; set; }
            /// <summary>
            /// 父部门id
            /// </summary>
            [Column("parent_id")]
            public string ParentId { get; set; }
            /// <summary>
            /// 祖级列表
            /// </summary>
            [Column("ancestors")]
            public string Ancestors { get; set; }
            /// <summary>
            /// 部门名称
            /// </summary>
            [Column("dept_name")]
            public string DeptName { get; set; }
            /// <summary>
            /// 显示顺序
            /// </summary>
            [Column("order_num")]
            public string OrderNum { get; set; }
            /// <summary>
            /// 负责人
            /// </summary>
            [Column("leader")]
            public string Leader { get; set; }
            /// <summary>
            /// 联系电话
            /// </summary>
            [Column("phone")]
            public string Phone { get; set; }
            /// <summary>
            /// 邮箱
            /// </summary>
            [Column("email")]
            public string Email { get; set; }
            /// <summary>
            /// 部门状态（0正常 1停用）
            /// </summary>
            [Column("status")]
            public char Status { get; set; }
            /// <summary>
            /// 删除标志（0代表存在 2代表删除）
            /// </summary>
            [Column("del_flag")]
            public char DelFlag { get; set; }
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

        private List<SysDept> children = new List<SysDept>();
        public List<SysDept> getChildren()
        {
            return children;
        }

        public void setChildren(List<SysDept> children)
        {
            this.children = children;
        }
    }
          
}
