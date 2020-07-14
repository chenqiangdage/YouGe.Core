using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_role")]
    public class SysRole:BaseModel<int>
    {
        /// <summary>
        /// 访问ID
        /// </summary>
        [Column("role_id")]
        public override int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Column("role_name")]
        public string RoleName { get; set; }
        /// <summary>
        /// 角色权限字符串
        /// </summary>
        [Column("role_key")]
        public string RoleKey { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        [Column("role_sort")]
        public int RoleSort { get; set; }
        /// <summary>
        /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限）
        /// </summary>
        [Column("data_scope")]
        public char dataScope { get; set; }
        /// <summary>
        /// 删除标志（0代表存在 2代表删除）
        /// </summary>
        [Column("del_flag")]
        public char DelFlag { get; set; }

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
        public SysRole()
        {
        }
    }
}
