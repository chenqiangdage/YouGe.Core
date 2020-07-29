using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Models.DTModel.Sys
{
    /// <summary>
    /// Treeselect树结构实体类
    /// </summary>
    public class TreeSelect
    {
        /// <summary>
        ///   路由名字
        /// <summary>
        public string name { get; set; }

        /// <summary>
        ///   路由地址
        /// <summary>
        public string path { get; set; }

        /// <summary>
        ///   是否隐藏路由，当设置 true 的时候该路由不会再侧边栏出现
        /// <summary>
        public bool hidden { get; set; }

        /// <summary>
        ///   重定向地址，当设置 noRedirect 的时候该路由在面包屑导航中不可被点击
        /// <summary>
        public string redirect { get; set; }

        /// <summary>
        ///   组件地址
        /// <summary>
        public string component { get; set; }

        /// <summary>
        ///   当你一个路由下面的 children 声明的路由大于1个时，自动会变成嵌套的模式--如组件页面
        /// <summary>
        public bool alwaysShow { get; set; }

        /// <summary>
        ///   其他元素
        /// <summary>
        public MetaVo meta { get; set; }

        /// <summary>
        ///   子路由
        /// <summary>
        public List<RouterVo> children { get; set; }        
    }
}
