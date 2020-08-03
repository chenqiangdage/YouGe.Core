using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YouGe.Core.Common.SystemConst
{
    public enum UserStatus
    {
        [Description("正常")]
        /// <summary>
        /// 正常
        /// </summary>
        OK = 0,
        [Description("停用")]
        /// <summary>
        /// 停用
        /// </summary>
        DISABLE = 1,
        [Description("删除")]
        /// <summary>
        /// 删除
        /// </summary>
        DELETED = 2
    }
}
