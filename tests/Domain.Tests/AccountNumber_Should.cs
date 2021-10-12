using Shouldly;
using System;
using Xunit;

namespace Domain.Tests
{
    public class AccountNumber_Should
    {
        [Fact]
        public void Create_AccountNumber_When_Valid()
        {
            const string validIban = "123-4567890-02";

            var ac = new AccountNumber(validIban);

            ac.Number.ShouldBe(validIban);
        }

        [Theory]
        [InlineData("123-4567890-12")] // Not valid MOD 97
        [InlineData("12-4567890-12")] // Not valid Format
        [InlineData("")] // Empty
        public void Throw_When_Invalid(string number)
        {
            Should.Throw<ArgumentException>(() => {
                var an = new AccountNumber(number);
            });
        }
    }
}
