﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Common.Helper
{
    public class YouGeBase64
    {
        ///编码
        ///编码
        public static string EncodeBase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        ///解码
        public static string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

        public static string encode(byte[] bytes)
        {
            string encode = "";
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = "";
            }
            return encode;
        }
    }
}
