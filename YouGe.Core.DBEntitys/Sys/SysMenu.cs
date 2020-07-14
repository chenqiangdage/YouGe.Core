using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_menu")]
    public class SysMenu :BaseModel<int>
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [Column("menu_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Column("menu_name")]
        public string MenuName { get; set; }
        /// <summary>
        /// 父菜单ID
        /// </summary>
        [Column("parent_id")]
        public int ParentId { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        [Column("order_num")]
        public int OrderNum { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>
        [Column("path")]
        public string Path { get; set; }
        /// <summary>
        /// 组件路径
        /// </summary>
        [Column("component")]
        public string Component { get; set; }
        /// <summary>
        /// 是否为外链（0是 1否）
        /// </summary>
        [Column("is_frame")]
        public string Isframe { get; set; }
        /// <summary>
        /// 菜单类型（M目录 C菜单 F按钮）
        /// </summary>
        [Column("menu_type")]
        public char MenuType { get; set; }
        /// <summary>
        /// 菜单状态（0显示 1隐藏）
        /// </summary>
        [Column("visible")]
        public string Visible { get; set; }
        /// <summary>
        /// 菜单状态（0正常 1停用）
        /// </summary>
        [Column("status")]
        public string Status { get; set; }
        /// <summary>
        /// 权限标识
        /// </summary>
        [Column("perms")]
        public string Perms { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        [Column("icon")]
        public string Icon { get; set; }
       
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
        public SysMenu()
        {
        }
    }
}
