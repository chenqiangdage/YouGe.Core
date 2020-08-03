using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Common.YouGeException
{
  public   class BaseException: SystemException
    {
        private static readonly long serialVersionUID = 1L;

         /// <summary>
           ///  所属模块
          /// <summary>
        private string module;

         /// <summary>
           ///  错误码
          /// <summary>
        private string code;

         /// <summary>
           ///  错误码对应的参数
          /// <summary>
        private object[] args;

         /// <summary>
           ///  错误消息
          /// <summary>
        private string defaultMessage;

        public BaseException(string module, string code, object[] args, string defaultMessage)
        {
            this.module = module;
            this.code = code;
            this.args = args;
            this.defaultMessage = defaultMessage;
        }

        public BaseException(string module, string code, object[] args): this(module, code, args, null)
        {            
        }

        public BaseException(string module, string defaultMessage):this(module, null, null, defaultMessage)
        {
         
        }

        public BaseException(string code, object[] args):this(null, code, args, null)
        {
            
        }

        public BaseException(string defaultMessage): this(null, null, null, defaultMessage)
        {
           
        }

          
        public string getModule()
        {
            return module;
        }

        public string getCode()
        {
            return code;
        }

        public object[] getArgs()
        {
            return args;
        }

        public string getDefaultMessage()
        {
            return defaultMessage;
        }
    }
}
