using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using YouGe.Core.DBEntitys.Sys;

using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Interface.IDbContexts;

namespace YouGe.Core.DbContexts
{
    public class YouGeDbContext : MySqlDbContext, IYouGeDbContext
    {
        public YouGeDbContext(YouGeDbContextOption option) : base(option)
        {
        }
        public YouGeDbContext(IOptions<YouGeDbContextOption> option) : base(option)
        {
        }

        public virtual DbSet<SysConfig>  SysConfig{ get; set; }
        public virtual DbSet<SysDept>  SysDepts { get; set; }
        public virtual DbSet<SysDictData> SysDictData { get; set; }
        public virtual DbSet<SysDictType> SysDictType { get; set; }
        public virtual DbSet<SysJob> SysJob { get; set; }
        public virtual DbSet<SysJobLog> SysJobLog { get; set; }
        public virtual DbSet<SysLoginInfor> SysLoginInfor { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysNotice> SysNotice { get; set; }
        public virtual DbSet<SysOperLog> SysOperLog { get; set; }
        public virtual DbSet<SysPost> SysPost { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysRoleDept> SysRoleDept { get; set; }
        public virtual DbSet<SysRoleMenu> SysRoleMenu { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SysUserPost> SysUserPost { get; set; }
        public virtual DbSet<SysUserRole> SysUserRole { get; set; }
    }
}
