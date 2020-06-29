using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Commons.Random
{
   public class OrderIDCreator
    {

        public static string CreateID()
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }

        public static string CreateID(int length)
        {
            return CreateID().Substring(0,(length>32?32:length));
        }

        public static string CreateID(string Pre)
        {
            return Pre + CreateID();
        }

        public static string CreateID(string Pre, int length)
        {
            return Pre + CreateID(length);
        }

        public static string CreateID(string Pre, int length, string timeFormat)
        {
            return Pre + DateTime.Now.ToString(timeFormat) + CreateID(length);
        }
    }
}
