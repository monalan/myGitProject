using System;
using System.Security.Cryptography;
using System.Text;

namespace Component.Encrypt
{
    public class CMD5
    {
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="md5Str">待加密字符</param>
        /// <returns></returns>
        public static string MD5String(string md5Str)
        {
            string str = null;
            try
            {

                byte[] buffer = System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(md5Str));
                for (int i = 0; i < buffer.Length; i++)
                {
                    string str3 = buffer[i].ToString("X");
                    if (str3.Length == 1)
                    {
                        str3 = "0" + str3;
                    }
                    str = str + str3;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return str;
        }

        /// <summary>
        /// 加
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string str, string key)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(key);
            byte[] rgbIV = encoding.GetBytes(key.ToUpper());
            ICryptoTransform transform = provider.CreateEncryptor(bytes, rgbIV);
            byte[] inputBuffer = encoding.GetBytes(str);
            return Convert.ToBase64String(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
        }

        /// <summary>
        /// 解
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decryptor(string str, string key)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(key);
            byte[] rgbIV = encoding.GetBytes(key.ToUpper());
            ICryptoTransform transform = provider.CreateDecryptor(bytes, rgbIV);
            byte[] inputBuffer = Convert.FromBase64String(str);
            byte[] buffer4 = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return encoding.GetString(buffer4);
        }
    }
}
