using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Tiny.OPS.Common
{
    /// <summary>
    /// https://www.cnblogs.com/duanjt/p/11061380.html
    /// </summary>
    public class RSACrypt
    {
        /// <summary>
        /// 生成一对公钥和私钥
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, string> GetKeyPair1()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            string public_Key = Convert.ToBase64String(RSA.ExportCspBlob(false));
            string private_Key = Convert.ToBase64String(RSA.ExportCspBlob(true));
            return new KeyValuePair<string, string>(public_Key, private_Key);
        }
        /// <summary>
        /// 生成一对公钥和私钥
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, string> GetKeyPair2()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            string public_Key = RSA.ToXmlString(false);
            string private_Key = RSA.ToXmlString(true);
            return new KeyValuePair<string, string>(public_Key, private_Key);
        }
    }

    //加密方法，加密方法RSA.Encrypt，解密方法RSA.Decrypt
    ////方法1
    //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
    //rsa.ImportCspBlob(Convert.FromBase64String(str_Public_Key));
    ////方法2
    //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
    //rsa.FromXmlString(privateKey);

}
