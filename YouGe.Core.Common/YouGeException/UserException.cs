using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Common.YouGeException
{
    public  class UserException:BaseException
    {
        private static readonly long serialVersionUID = 1L;         
        public UserException(string code, object[] args):base("user", code, args, null)
        {            
        }
    }
}
