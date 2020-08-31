using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.DBEntitys;
using YouGe.Core.DBEntitys.Sys;

namespace YouGe.Core.Models.System
{
    public class TreeSelect
    {
        private static readonly long serialVersionUID = 1L;

        /** 节点ID */
        private long id { get; set; }

        /** 节点名称 */
        private string label { get; set; }

        /** 子节点 */        
    private List<TreeSelect> children { get; set; }

        public TreeSelect()
        {

        }

        public TreeSelect(SysDept dept)
        {
            this.id = dept.Id;
            this.label = dept.DeptName;
            this.children = dept.getChildren().stream().map(TreeSelect::new).collect(Collectors.toList());
        }

        public TreeSelect(SysMenu menu)
        {
            this.id = menu.Id;
            this.label = menu.MenuName;
            this.children = menu.getChildren().stream().map(TreeSelect::new).collect(Collectors.toList());
        }                     
    }
}
