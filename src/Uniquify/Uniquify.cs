using System.Security.Cryptography;
using Uniquify.Number;
using Uniquify.String;

namespace Uniquify
{
    public static class Uniquify
    {
        public static string GetString(this RNGCryptoServiceProvider rngService, int length) => StringGenerator.UniqueString(rngService, length);
       
        public static string GetString(this RNGCryptoServiceProvider rngService, string allowedChars, int length) => StringGenerator.UniqueString(rngService, allowedChars, length);

        public static string GetString(int length)
        {
            using var rngService = new RNGCryptoServiceProvider();
            return StringGenerator.UniqueString(rngService, length);
        }

        public static string GetString(string allowedChars, int length)
        {
            using var rngService = new RNGCryptoServiceProvider();
            return StringGenerator.UniqueString(rngService, allowedChars, length);
        }

        public static long GetInt64(this RNGCryptoServiceProvider rngService, int length) => NumberGenerator.GenerateUniqueInt64(rngService, length);
     
        public static int GetInt32(this RNGCryptoServiceProvider rngService, int length) => NumberGenerator.GenerateUniqueInt32(rngService, length);
     
        public static long GetInt64(int length)
        {
            using var rngService = new RNGCryptoServiceProvider();
            return NumberGenerator.GenerateUniqueInt64(rngService, length);
        }

        public static int GetInt32(int length)
        {
            using var rngService = new RNGCryptoServiceProvider();
            return NumberGenerator.GenerateUniqueInt32(rngService, length);
        }
    }
}