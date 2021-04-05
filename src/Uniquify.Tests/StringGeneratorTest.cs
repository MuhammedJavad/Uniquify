using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using FluentAssertions;
using Xunit;

namespace Uniquify.Tests
{
    public class StringGeneratorTest
    {
        [Theory]
        [InlineData(12)]
        [InlineData(32)]
        [InlineData(112)]
        [InlineData(2)]
        [InlineData(1000)]
        public void GenerateString_ShouldBeSuccessful(int length)
        {
            var uniqueString = Uniquify.GetString(length);
        
            uniqueString.Should().NotBeEmpty().And.HaveLength(length);
        }

        [Fact]
        public void GenerateOneMillionString_WithRngProvider_ShouldBeUnique()
        {
            const int count = 1000000;
            var holder = new string[count];
            var rnd = new Random();

            using var rngProvider = new RNGCryptoServiceProvider();

            for (var i = 0; i < count; i++) 
                holder[i] = rngProvider.GetString(rnd.Next(10, 30));

            holder.Should().NotBeEmpty().And.NotContainNulls().And.HaveCount(count).And.OnlyHaveUniqueItems();
        }

        [Theory]
        [InlineData(100, "RSEDTFGHJKLhdgfuakydsgfuayksdjfbjka53624351!#@!$")]
        [InlineData(17, "12351345143")]
        [InlineData(9, "!@#$%^&*()_+~")]
        public void GenerateCustomAlphabetString_ShouldBeSuccessful(int length, string chars)
        {
            var uniqueString = Uniquify.GetString(chars, length);
            uniqueString.Should().NotBeEmpty().And.HaveLength(length).And.ContainAny(chars.Select(c => c.ToString()).ToArray());
        }

        [Fact]
        public void GenerateString_WithSingleChar_ShouldBeSuccessful()
        {
            var uniqueString = Uniquify.GetString("q", 12);

            uniqueString.Should().NotBeEmpty().And.HaveLength(12).And.Be("qqqqqqqqqqqq");
        }
    }
}