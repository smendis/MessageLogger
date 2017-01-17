using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLogger.Web.Helpers
{
    public class Base64Utility
    {
        public static string Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }


        public static string Decode(string encodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}