using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Common.Helper;

namespace YouGe.Core.Models.System
{
    public class RequestBasicInfo
    {
        public string Ip { get; set; }

        public DateTime RequestTime { get; set; }

        public string RequestType { get; set; }

        public string RequestUrl { get; set; }

        public UAParserUserAgent UserAgent { get; set; }
    }
}
