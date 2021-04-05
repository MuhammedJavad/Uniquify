using System;
using System.Security.Cryptography;

namespace Uniquify.String
{
    internal class StringGenerator
    {
        private const string DefaultChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789-_";
        
        internal static string UniqueString(RNGCryptoServiceProvider rngService, int length) => UniqueString(rngService, DefaultChars, length);

        internal static string UniqueString(RNGCryptoServiceProvider rngService, string allowedChars, int length)
        {
            if (rngService == null) throw new ArgumentNullException(nameof(rngService));
            if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentNullException(nameof(allowedChars));
            if (allowedChars.Length > 255) throw new ArgumentOutOfRangeException(nameof(allowedChars), "must contain between 1 and 255 symbols.");
            if (length < 1 || length > 1000) throw new ArgumentOutOfRangeException(nameof(length), "Your specified length should be between 1 and 1000");

            // See https://github.com/ai/nanoid/blob/7bf7300f2bf646c6fd722382ddb3202e58251c9b/index.js#L13 for understanding usage of bitmask
            var mask = (2 << 31 - GetLeadingZerosForInt32((allowedChars.Length - 1) | 1)) - 1;
            var step = (int) Math.Ceiling(1.6 * mask * length / allowedChars.Length);

            Span<char> strBuilder = stackalloc char[length];
            Span<byte> randomBytes = stackalloc byte[step];

            var counter = 0;
            while (true)
            {
                rngService.GetBytes(randomBytes);

                for (var i = 0; i < step; i++)
                {
                    var charIndex = randomBytes[i] & mask;

                    if (charIndex >= allowedChars.Length) continue;
                    
                    strBuilder[counter] = allowedChars[charIndex];
                    
                    if (++counter == length) return new string(strBuilder);
                }
            }
        }

        private static int GetLeadingZerosForInt32(int number)
        {
            var uValue = (uint) number;
            var leadingZeros = 0;
            while (uValue != 0)
            {
                uValue >>= 1;
                leadingZeros++;
            }
            
            return 32 - leadingZeros;
        }
    }
}