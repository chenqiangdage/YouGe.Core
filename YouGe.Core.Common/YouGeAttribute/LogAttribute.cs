
using System;

namespace YouGe.Core.Common.YouGeAttribute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class YouGeLogAttribute : Attribute
    {
        public string title;
        public BusinessType buinessType;
       
       
       // public YouGeLogAttribute(string title, BusinessType buinessType)
       // {
       //     this.Title = title;
       //     this.BuinessType = buinessType;
       // }

        
    }
}
