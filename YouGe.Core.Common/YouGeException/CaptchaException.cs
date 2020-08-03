using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Common.YouGeException
{
    public class CaptchaException :UserException
    {
        private static readonly long serialVersionUID = 1L;

        public CaptchaException():base("user.jcaptcha.error", null)
        {            
        }
    }
}
