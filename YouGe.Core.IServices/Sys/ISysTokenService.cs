using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Models.DTModel.Sys;

namespace YouGe.Core.Interface.IServices.Sys
{
    public  interface ISysTokenService
    {
        public LoginUser getLoginUser(HttpRequest request);
    }
}
