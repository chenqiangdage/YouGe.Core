using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YouGe.Core.Interface.IServices.IManager;
using YouGe.Core.Models.System;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Commons.SystemConst;
using YouGe.Core.Interface.IServices.Sys;
using YouGe.Core.DBEntitys.Sys;
using Microsoft.AspNetCore.Http;
using YouGe.Core.Commons;
using YouGe.Core.Common.Helper;
using System.Buffers.Text;
using YouGe.Core.Commons.Helper;
using System.IO;

namespace YouGe.Core.ManagerApi.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CaptchaController : YouGeController
    {
        /// <summary>
        /// 
        /// </summary>
        public CaptchaController()
        {
            
        }

        /// <summary>
        /// 生成验证码
        /// </summary>        
        [AllowAnonymous]
        [HttpGet("captchaImage")]      
        public AjaxReponseBase getCode() 
        {
            // 生成随机字串
            string verifyCode = string.Empty;
        // 唯一标识
        string uuid = Guid.NewGuid().ToString().Replace("-","");
        string verifyKey = YouGeSystemConst.CAPTCHA_CODE_KEY + uuid;

        
        // 生成图片
        int w = 111, h = 36;
        MemoryStream stream = new MemoryStream();
        VerifyCodeUtils.outputImage(w, h, out verifyCode, 4);
        YouGeRedisHelper.Set(verifyKey, verifyCode, YouGeSystemConst.CAPTCHA_EXPIRATION * 60);
            try
        {
            AjaxReponseBase ajax = AjaxReponseBase.Success();                 
            ajax.Add("uuid", uuid);
            ajax.Add("img", YouGeBase64.encode(stream.ToBytes()));
            return ajax;
        }
        catch (Exception e)
        {
             Log4NetHelper.Error("获取验证码异常 "+ e.StackTrace);
            return AjaxReponseBase.Error(e.Message);
        }
        finally
        {
            stream.Close();
        }
    }
    }
}
