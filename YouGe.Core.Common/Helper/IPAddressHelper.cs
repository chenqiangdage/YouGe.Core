using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using YouGe.Core.Commons.Helper;
using YouGe.Core.Commons.SystemConst;
using YouGe.Core.Commons;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace YouGe.Core.Common.Helper
{
   public  class IPAddressHelper
    {
         

    // IP地址查询
    public static readonly string IP_URL = "http://whois.pconline.com.cn/ipJson.jsp";

    // 未知地址
    public static readonly string UNKNOWN = "XX XX";

    public static async Task<string> getRealAddressByIP(string ip)
        {
            string address = UNKNOWN;
            // 内网不查询
            if (IpUtils.internalIp(ip))
            {
                return "内网IP";
            }
            
             
                try
                {
                HttpClient client = new HttpClient();
                Dictionary<string, string> parmas = new Dictionary<string, string>();
                parmas.Add("ip",ip);
                parmas.Add("json", "true");
                 
                string rspStr = await client.GetJsonAsync(parmas, IP_URL);
                
                    if (string.IsNullOrEmpty(rspStr))
                    {
                    Log4NetHelper.Info(string.Format("获取地理位置异常 {0}", ip));
                    return UNKNOWN;
                    }
                    JObject obj = (JObject)JsonConvert.DeserializeObject(rspStr);
                    string region = obj["pro"].ToString();
                    string city = obj["city"].ToString();
                    return string.Format("{0} {1}", region, city);
                }
                catch (Exception e)
                {
                    Log4NetHelper.Info(string.Format("获取地理位置异常 {0} {1}", ip,e.Message));
                }            
            return address;
        }
    }
}
