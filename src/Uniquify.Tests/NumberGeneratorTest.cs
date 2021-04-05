using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Uniquify.Tests
{

    public class NumberGeneratorTest
    {
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(8)]
        public void GenerateInt32_ShouldBeSuccessful(int length)
        {
            var randNumber = Uniquify.GetInt32(length);

            randNumber.ToString().Should().HaveLength(length);
            randNumber.Should().BeOfType(typeof(int)).And.NotBe(0);
        }

        [Fact]
        public void GenerateOneMillionInt32_WithRngProvider_ShouldBeUnique()
        {
            const int count = 10000;
            var numbersHolder = new int[count];

            using var rngProvider = new RNGCryptoServiceProvider();

            for (var i = 0; i < count; i++) 
                numbersHolder[i] = rngProvider.GetInt32(9);

            numbersHolder.Should().NotBeEmpty().And.NotContainNulls().And.HaveCount(count).And.OnlyHaveUniqueItems();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(8)]
        [InlineData(12)]
        [InlineData(16)]
        [InlineData(18)]
        public void GenerateInt64_ShouldBeSuccessful(int length)
        {
            var randNumber = Uniquify.GetInt64(length);

            randNumber.ToString().Should().HaveLength(length);
            randNumber.Should().BeOfType(typeof(long)).And.NotBe(0);
        }

        [Fact]
        public void GenerateOneMillionInt64_WithRngProvider_ShouldBeUnique()
        {
            const int count = 1000000;
            var numbersHolder = new long[count];

            using var rngProvider = new RNGCryptoServiceProvider();

            for (var i = 0; i < count; i++) 
                numbersHolder[i] = rngProvider.GetInt64(13);

            numbersHolder.Should().NotBeEmpty().And.NotContainNulls().And.HaveCount(count).And.OnlyHaveUniqueItems();
        }

        [Theory]
        [InlineData(12)]
        [InlineData(10)]
        [InlineData(18)]
        public void GenerateInt32_WithOutOfRangeLength_ShouldBeFailed(int length)
        {
            Action waitingForException = () => Uniquify.GetInt32(length);
            waitingForException.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(21)]
        [InlineData(32)]
        [InlineData(19)]
        public void GenerateInt64_WithOutOfRangeLength_ShouldBeFailed(int length)
        {
            Action waitingForException = () => Uniquify.GetInt64(length);
            waitingForException.Should().Throw<ArgumentOutOfRangeException>();
        }


    }
}