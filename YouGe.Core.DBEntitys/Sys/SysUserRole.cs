using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_user_role")]
    public class SysUserRole:BaseModel<int>
    {
        /// <summary>
        /// 访问ID
        /// </summary>
        [Column("user_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        [Column("role_id")]
        public int PostId { get; set; }
        public SysUserRole()
        {
        }
    }
}
