using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Common.YouGeException
{ 
   
    public class UsernameNotFoundException : CustomException
    {
        private static readonly long serialVersionUID = 1L;

        
        public UsernameNotFoundException(string message) : base( message)
        {
        }
    }
}
