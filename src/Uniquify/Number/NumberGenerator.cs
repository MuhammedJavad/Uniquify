using System;
using System.Security.Cryptography;

namespace Uniquify.Number
{
    internal class NumberGenerator
    {

        internal static long GenerateUniqueInt64(RNGCryptoServiceProvider rngService, int length)
        {
            if (rngService == null) throw new ArgumentNullException(nameof(rngService));
            if (length > 18) throw new ArgumentOutOfRangeException(nameof(length), "Can not generate a number with a length greater than 18");

            Span<byte> bytes = stackalloc byte[sizeof(long)];

            rngService.GetNonZeroBytes(bytes);

            return SlideNumber(BitConverter.ToInt64(bytes), length);
        }

        internal static int GenerateUniqueInt32(RNGCryptoServiceProvider rngService, int length)
        {
            if (rngService == null) throw new ArgumentNullException(nameof(rngService));
            if (length > 9) throw new ArgumentOutOfRangeException(nameof(length), "Can not generate a number with a length greater than 18");

            Span<byte> bytes = stackalloc byte[sizeof(long)];

            rngService.GetNonZeroBytes(bytes);

            return (int) SlideNumber(BitConverter.ToInt32(bytes), length);
        }

        private static long SlideNumber(long number, int length)
        {
            var min = (long) Math.Pow(10, length - 1);
            var max = (long) Math.Pow(10, length) - 1;
            var value = ((number - min) % (max - min + 1) + max - min + 1) % (max - min + 1) + min;

            return Math.Abs(value);
        }
    }
}