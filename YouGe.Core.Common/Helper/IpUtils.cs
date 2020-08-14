using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using YouGe.Core.Common.Extensions;
using YouGe.Core.Commons;

namespace YouGe.Core.Common.Helper
{
    public class IpUtils
    {
        public static string getIpAddr(IHttpContextAccessor httpContextAccessor )
        {
            var request = httpContextAccessor.HttpContext.Request;
            if (request == null)
            {
                return "unknown";
            }
            string ip = request.Headers["x-forwarded-for"];
            if (ip == null || ip.Length == 0 || "unknown".Equals(ip.ToLower()))
            {
                ip = request.Headers["Proxy-Client-IP"];
            }
            if (ip == null || ip.Length == 0 || "unknown".Equals(ip.ToLower()))
            {
                ip = request.Headers["X-Forwarded-For"];
            }
            if (ip == null || ip.Length == 0 || "unknown".Equals(ip.ToLower()))
            {
                ip = request.Headers["WL-Proxy-Client-IP"];
            }
            if (ip == null || ip.Length == 0 || "unknown".Equals(ip.ToLower()))
            {
                ip = request.Headers["X-Real-IP"];
            }

            if (ip == null || ip.Length == 0 || "unknown".Equals(ip.ToLower()))
            {
                ip = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            return "0:0:0:0:0:0:0:1".Equals(ip) ? "127.0.0.1" : ip.StripHtml();
        }

        public static bool internalIp(string ip)
        {            
            return checkInternalIp(ip) || "127.0.0.1".Equals(ip) || "0.0.0.1".Equals(ip);
        }

        private static bool checkInternalIp(string ipv4Address)
        {
            if (IPAddress.TryParse(ipv4Address, out var ip))
            {
                byte[] ipBytes = ip.GetAddressBytes();
                if (ipBytes[0] == 10) return true;
                if (ipBytes[0] == 172 && ipBytes[1] >= 16 && ipBytes[1] <= 31) return true;
                if (ipBytes[0] == 192 && ipBytes[1] == 168) return true;
            }

            return false;
        }

        /**
         * 将IPv4地址转换成字节
         * 
         * @param text IPv4地址
         * @return byte 字节
         */
        public static byte[] textToNumericFormatV4(string text)
        {
            if (text.Length == 0)
            {
                return null;
            }

            byte[] bytes = new byte[4];
            string[] elements = text.Split("\\.", -1);
            try
            {
                long l;
                int i;
                switch (elements.Length)
                {
                    case 1:
                        l = long.Parse(elements[0]);
                        if ((l < 0L) || (l > 4294967295L))
                        {
                            return null;
                        }
                        bytes[0] = (byte)(int)(l >> 24 & 0xFF);
                        bytes[1] = (byte)(int)((l & 0xFFFFFF) >> 16 & 0xFF);
                        bytes[2] = (byte)(int)((l & 0xFFFF) >> 8 & 0xFF);
                        bytes[3] = (byte)(int)(l & 0xFF);
                        break;
                    case 2:
                        l = int.Parse(elements[0]);
                        if ((l < 0L) || (l > 255L))
                        {
                            return null;
                        }
                        bytes[0] = (byte)(int)(l & 0xFF);
                        l = int.Parse(elements[1]);
                        if ((l < 0L) || (l > 16777215L))
                        {
                            return null;
                        }
                        bytes[1] = (byte)(int)(l >> 16 & 0xFF);
                        bytes[2] = (byte)(int)((l & 0xFFFF) >> 8 & 0xFF);
                        bytes[3] = (byte)(int)(l & 0xFF);
                        break;
                    case 3:
                        for (i = 0; i < 2; ++i)
                        {
                            l = int.Parse(elements[i]);
                            if ((l < 0L) || (l > 255L))
                            {
                                return null;
                            }
                            bytes[i] = (byte)(int)(l & 0xFF);
                        }
                        l = int.Parse(elements[2]);
                        if ((l < 0L) || (l > 65535L))
                        {
                            return null;
                        }
                        bytes[2] = (byte)(int)(l >> 8 & 0xFF);
                        bytes[3] = (byte)(int)(l & 0xFF);
                        break;
                    case 4:
                        for (i = 0; i < 4; ++i)
                        {
                            l = int.Parse(elements[i]);
                            if ((l < 0L) || (l > 255L))
                            {
                                return null;
                            }
                            bytes[i] = (byte)(int)(l & 0xFF);
                        }
                        break;
                    default:
                        return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return bytes;
        }

       

     
    }
}
