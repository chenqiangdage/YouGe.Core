using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Models.DTModel.Sys
{
    /// <summary>
    /// 路由显示信息
    /// </summary>
    public class MetaVo
    {
        /// <summary>
        /// 设置该路由在侧边栏和面包屑中展示的名字
        /// <summary>
        public string title { get; set; }

        /// <summary>
        /// 设置该路由的图标，对应路径src/icons/svg
        /// <summary>
        public  string icon { get; set; }

        public MetaVo()
        {
        }

        public MetaVo(string title, string icon)
        {
        this.title = title;
        this.icon = icon;
        }          
    }
}
