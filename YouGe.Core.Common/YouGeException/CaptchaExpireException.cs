using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Common.YouGeException
{
    public  class CaptchaExpireException :UserException
    {
        private static readonly long serialVersionUID = 1L;

        public CaptchaExpireException():base("user.jcaptcha.error", null)
        {            
        }
    }
}
