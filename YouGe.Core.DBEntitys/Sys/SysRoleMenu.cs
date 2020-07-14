using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_role_menu")]
    public class SysRoleMenu:BaseModel<int>
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
        [Column("menu_id")]
        public int MenuId { get; set; }
        
        public SysRoleMenu()
        {
        }
    }
}
