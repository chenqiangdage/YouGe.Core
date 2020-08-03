using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Common.YouGeException
{
    public class UserPasswordNotMatchException:UserException
    {
        private static readonly long serialVersionUID = 1L;

        public UserPasswordNotMatchException(): base("user.password.not.match", null)
        {         
        }
    }
}
