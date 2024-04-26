using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionAndDecryption
{
    public class RSAExtend
    {
        public  void Show()
        {
            string Str = "Hello World";
            Console.WriteLine("加密前数据："+Str);
            RsaEncrypt _RsaEncrypt = new RsaEncrypt();
            KeyValuePair<string, string> keyValuePairs = _RsaEncrypt.GetKeyValue();
            KeyValuePair<string, string> keyValuePairs1 = _RsaEncrypt.GetKeyValue();
            string GetRsa = _RsaEncrypt.GetRSA(Str, keyValuePairs.Key, Encoding.UTF8);
            Console.WriteLine($"加密后数据：{GetRsa}");
            string OpenRsa = _RsaEncrypt.OpenRSA(GetRsa, keyValuePairs.Value, Encoding.UTF8);
            Console.WriteLine($"解密后数据：{OpenRsa}");
        }
    }
    public class RsaEncrypt
    {
        /// <summary>
        /// 获取公钥和私钥
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, string> GetKeyValue()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string publicKey = rsa.ToXmlString(false);//公钥
            string privateKey = rsa.ToXmlString(true);//私钥
            return new KeyValuePair<string, string>(publicKey, privateKey);
        }
        /// <summary>
        ///字符加密
        /// </summary>
        /// <param name="input">加密字符</param>
        /// <param name="key">加密key</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public string GetRSA(string input, string key, Encoding encoding)
        {
            // 创建RSA实例
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(key);
                // 加密数据
                byte[] data = encoding.GetBytes(input);
                byte[] encryptedData = rsa.Encrypt(data, false);
                string GetStr = Convert.ToBase64String(encryptedData);
                return GetStr;
            }
        }
        /// <summary>
        /// 字符解密
        /// </summary>
        /// <param name="input">解密字符</param>
        /// <param name="value">解密value</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public string OpenRSA(string input, string value, Encoding encoding)
        {
            try
            {
                // 创建RSA实例
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(value);
                    // 解密数据
                    byte[] encryptedData = Convert.FromBase64String(input);
                    byte[] decryptedData = rsa.Decrypt(encryptedData, false);
                    string decryptedString = encoding.GetString(decryptedData);
                    return decryptedString;
                }
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.Message + "解密密钥错误");
            }
        }
    }
}

