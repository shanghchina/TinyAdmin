using System;
using System.IO;
using System.Security.Cryptography;

namespace OnlyEdu.Common.Encryption
{
    /// <summary>
    /// 
    /// </summary>
    public static class AESEncryption
    {

        /// <summary>
        /// 
        /// </summary>
        public struct AESKey
        {
            /// <summary>
            /// 
            /// </summary>
            public string Key { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string IV { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AESKey GetAesKey()
        {
            AESKey aesKey;
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.GenerateKey();
                aesAlg.GenerateIV();

                aesKey = new AESKey()
                {
                    Key = Convert.ToBase64String(aesAlg.Key),
                    IV = Convert.ToBase64String(aesAlg.IV)

                };
            }
            return aesKey;
        }
        /// <summary>
        /// 加密内容
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="keyText"></param>
        /// <param name="ivText"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string EncryptString(string plainText, string keyText, string ivText)
        {
            
            // Check arguments.
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (string.IsNullOrEmpty(keyText))
                throw new ArgumentNullException(nameof(keyText));
            if (string.IsNullOrEmpty(ivText))
                throw new ArgumentNullException(nameof(ivText));

            byte[] key = Convert.FromBase64String(keyText);
            byte[] iv = Convert.FromBase64String(ivText);

            byte[] encrypted;
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);

        }


        /// <summary>
        /// 解密内容
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="keyText"></param>
        /// <param name="ivText"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string DecryptString(string cipherText, string keyText, string ivText)
        {
            
            // Check arguments.
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));
            if (string.IsNullOrEmpty(keyText))
                throw new ArgumentNullException(nameof(keyText));
            if (string.IsNullOrEmpty(ivText))
                throw new ArgumentNullException(nameof(ivText));

            byte[] key = Convert.FromBase64String(keyText);
            byte[] iv = Convert.FromBase64String(ivText);
            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {

                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    }
}
