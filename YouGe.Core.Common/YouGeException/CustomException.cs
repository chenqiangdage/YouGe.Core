using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Common.YouGeException
{
    public class CustomException: SystemException
    {
        private static readonly long serialVersionUID = 1L;

        private int code;

        private string message;

        public CustomException(string message)
        {
            this.message = message;
        }

        public CustomException(string message, int code)
        {
            this.message = message;
            this.code = code;
        }

        public CustomException(string message, Exception e):base(message,e)
        {            
            this.message = message;
        }

 

        public int getCode()
        {
            return code;
        }
    }
}
