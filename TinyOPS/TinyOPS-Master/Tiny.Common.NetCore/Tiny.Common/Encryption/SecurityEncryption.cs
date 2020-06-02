using System;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace Tiny.Common.Encryption
{
   
    /// <summary>
    /// MD5、SHA1-Base64、随机
    /// </summary>
    public class SecurityEncryption
    {
        
        /// <summary>
        /// 计算SHA1并返回Base64
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String SHA1_Encrypt_String(string value)
        {
            byte[] HashValue;

            //Convert the string into an array of bytes.
            byte[] MessageBytes = Encoding.UTF8.GetBytes(value);

            //Create a new instance of the SHA1Managed class to create 
            //the hash value.
            using (SHA1Managed SHhash = new SHA1Managed())
            {
                HashValue = SHhash.ComputeHash(MessageBytes);
            }
            return Convert.ToBase64String(HashValue);
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static String MD5_Encrypt_String(string value)
        {
            byte[] t;
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {

                t = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 2009-12-13
        /// MD5加密
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static String MD5_Encrypt_String(byte[] buf)
        {
            byte[] t;
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                t = md5.ComputeHash(buf);

            }
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 2010-09-03
        /// MD5签名
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static Byte[] MD5_Encrypt_Byte(Byte[] buf)
        {
            Byte[] tmp;
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                tmp = md5.ComputeHash(buf);
            }
            return (tmp);
        }
        /// <summary>
        /// 将MD5值转为16进制
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static String MD5_ByteToStr(byte[] buf)
        {
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < buf.Length; i++)
            {
                sb.Append(buf[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        public static Byte[] MD5_Encrypt_Byte(string value)
        {
            byte[] t;
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {

                t = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            }

            return t;
        }
        /// <summary>
        /// 将16进制转回MD5
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Byte[] MD5_StrToByte(String value)
        {
            Byte[] t = new Byte[16];
            for (int i = 0, j = 0; i < value.Length; i += 2)
            {
                t[j] = Convert.ToByte(value.Substring(i, 2), 16);
                j++;
            }
            return t;
        }
        
       /// <summary>
        /// 2010-09-10
        /// 获得随机密钥
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static String GetEncodekey(String seed)
        {
            String tmpstr = MD5_Encrypt_String(Guid.NewGuid().ToString() + seed);
            return tmpstr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyLen"></param>
        /// <returns></returns>
        public static string CreateRandomKey(Int32 keyLen)
        {
            var bytes = new byte[keyLen];
            new RNGCryptoServiceProvider().GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
        //
    }
}
