using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace YouGe.Core.DBEntitys.Sys
{
    [Table("sys_user")]
    public class SysUser:BaseModel<int>
    {
        ///to do 联合主键怎么班？？？
        /// <summary>
        /// 角色ID
        /// </summary>
        [Column("user_id")]
        public override int Id { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        [Column("dept_id")]
        public int DeptId { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        [Column("user_name")]
        public string UserName { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        [Column("nick_name")]
        public string NickName { get; set; }
        /// <summary>
        /// 用户类型（00系统用户）
        /// </summary>
        [Column("user_type")]
        public string UserType { get; set; }
        /// <summary>
        /// email
        /// </summary>
        [Column("email")]
        public string Email { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Column("phonenumber")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 用户性别（0男 1女 2未知）
        /// </summary>
        [Column("sex")]
        public char Sex { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        [Column("avatar")]
        public string Avatar { get; set; }
        /// <summary>
        /// pwd
        /// </summary>
        [Column("password")]
        public string Password { get; set; }
        /// <summary>
        /// 最后登陆IP
        /// </summary>
        [Column("login_ip")]
        public string LoginIp { get; set; }
        /// <summary>
        /// 最后登陆IP
        /// </summary>
        [Column("login_Date")]
        public DateTime? LoginDate { get; set; }


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
        public SysUser()
        {
        }

        public bool isAdmin()
        {
            return (this.Id != 0 && this.Id == 1); 
        }

        
    }
}
