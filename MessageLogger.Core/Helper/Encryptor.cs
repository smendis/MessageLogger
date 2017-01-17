using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Core.Helper
{
    public class Encryptor
    {
        public static string Encrypt(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));//md5 encryption

            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digitsfor each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString().Substring(0, 25);
        }
    }
}
