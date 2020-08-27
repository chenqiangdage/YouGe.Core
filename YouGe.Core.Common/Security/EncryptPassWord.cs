using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace YouGe.Core.Common.Security
{
    public class EncryptPassWord
    {
        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public static string CreateSalt()
        {
            byte[] data = new byte[8];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="pwdString"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string EncryptPwd(string pwdString, string salt)
        {
            if (salt == null || salt == "")
            {
                return pwdString;
            }
            byte[] bytes = Encoding.Unicode.GetBytes(salt.ToLower().Trim() + pwdString.Trim());
            return BitConverter.ToString(((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(bytes));
        }



        private static string iv = "12345678";
        private static string key = "12345678";
        private static  Encoding encoding = new UnicodeEncoding();
        private static DES des;

        

        /// <summary>
        /// 设置加密密钥
        /// </summary>
        public static string EncryptKey
        {
            get { return key; }
            set
            {
                key = value;
            }
        }

        /// <summary>
        /// 要加密字符的编码模式
        /// </summary>
        public static  Encoding EncodingMode
        {
            get { return encoding; }
            set { encoding = value; }
        }

        /// <summary>
        /// 加密字符串并返回加密后的结果
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptString(string str)
        {
            byte[] ivb = Encoding.ASCII.GetBytes(iv);
            byte[] keyb = Encoding.ASCII.GetBytes(EncryptKey);//得到加密密钥
            byte[] toEncrypt = EncodingMode.GetBytes(str);//得到要加密的内容
            byte[] encrypted;
            ICryptoTransform encryptor = des.CreateEncryptor(keyb, ivb);
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();
            encrypted = msEncrypt.ToArray();
            csEncrypt.Close();
            msEncrypt.Close();
            return EncodingMode.GetString(encrypted);
        }
    }
}