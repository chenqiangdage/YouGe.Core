using System;
namespace YouGe.Core.Common.SystemConst
{
    public class YouGeUserConstants
    {
        
        /**
         * 平台内系统用户的唯一标志
         */
        public static readonly string SYS_USER = "SYS_USER";

    /** 正常状态 */
    public static readonly string NORMAL = "0";

    /** 异常状态 */
    public static readonly string EXCEPTION = "1";

    /** 用户封禁状态 */
    public static readonly string USER_DISABLE = "1";

    /** 角色封禁状态 */
    public static readonly string ROLE_DISABLE = "1";

    /** 部门正常状态 */
    public static readonly string DEPT_NORMAL = "0";

    /** 部门停用状态 */
    public static readonly string DEPT_DISABLE = "1";

    /** 字典正常状态 */
    public static readonly string DICT_NORMAL = "0";

    /** 是否为系统默认（是） */
    public static readonly string YES = "Y";

    /** 是否菜单外链（是） */
    public static readonly string YES_FRAME = "0";

    /** 是否菜单外链（否） */
    public static readonly string NO_FRAME = "1";

    /** 菜单类型（目录） */
    public static readonly string TYPE_DIR = "M";

    /** 菜单类型（菜单） */
    public static readonly string TYPE_MENU = "C";

    /** 菜单类型（按钮） */
    public static readonly string TYPE_BUTTON = "F";

    /** Layout组件标识 */
    public static readonly string    LAYOUT = "Layout";

        /** 校验返回结果码 */
        public   static readonly string UNIQUE = "0";
        public   static readonly string NOT_UNIQUE = "1";
    }

}
