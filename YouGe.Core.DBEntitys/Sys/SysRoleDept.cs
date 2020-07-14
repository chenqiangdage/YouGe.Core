using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_role_dept")]
    public class SysRoleDept:BaseModel<int>
    {
        ///to do 联合主键怎么班？？？
        /// <summary>
        /// 角色ID
        /// </summary>
        [Column("role_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        [Column("dept_id")]
        public int DeptId { get; set; }
        public SysRoleDept()
        {
        }
    }
}
