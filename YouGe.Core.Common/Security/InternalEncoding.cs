﻿using System.Text;

namespace YouGe.Core.Common.Security
{
    internal static class InternalEncoding
    {
        static InternalEncoding()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        internal static Encoding GetEncoding(string name)
        {
            return Encoding.GetEncoding(name);
        }

        internal static Encoding GetEncoding(int codepage)
        {
            return Encoding.GetEncoding(codepage);
        }
    }
}
