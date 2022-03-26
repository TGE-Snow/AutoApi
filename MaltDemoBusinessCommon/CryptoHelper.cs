using AiMiYun.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaltDemoBusinessCommon
{
    /// <summary>
    /// 加密解密
    /// </summary>
    public static class CryptoHelper
    {
        /// <summary>
        /// 加密(根据密钥)
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后文本</returns>
        public static string EncryptByKey(string text, string key)
        {
            return Crypto.Encrypt(text, GetKeyByText(key));
        }

        /// <summary>
        /// 解密(根据密钥)
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后文本</returns>
        public static string DecryptByKey(string text, string key)
        {
            return Crypto.Decrypt(text, GetKeyByText(key));
        }

        private static byte[] GetKeyByText(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                string key = MD5(text).ToUpper();
                return Encoding.UTF8.GetBytes(key);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 加密(根据日期时间)
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="dateFormat">日期格式</param>
        /// <returns>加密后文本</returns>
        public static string EncryptByDate(string text, string dateFormat = "Md")
        {
            return Crypto.Encrypt(text, GetKeyByDate(dateFormat));
        }

        /// <summary>
        /// 解密(根据日期时间)
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="dateFormat">日期格式</param>
        /// <returns>解密后文本</returns>
        public static string DecryptByDate(string text, string dateFormat = "Md")
        {
            return Crypto.Decrypt(text, GetKeyByDate(dateFormat));
        }

        private static byte[] GetKeyByDate(string dateFormat)
        {
            string date = DateTime.Now.ToString(dateFormat);
            string key = MD5(date).ToUpper();
            return Encoding.UTF8.GetBytes(key);
        }

        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>加密后文本</returns>
        public static string MD5(string text)
        {
            return BitConverter.ToString(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", null);
        }
    }
}
