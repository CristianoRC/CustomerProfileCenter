using CustomerProfileCenter.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CustomerProfileCenter.UnitTest.Domain.ValueObjects;

public class PhoneNumberUnitTest
{
    [Theory(DisplayName = "If Phone Number Is Invalid, Should Set As Invalid")]
    [InlineData("+55 99 542343848")]
    [InlineData("1asdf44")]
    [InlineData("06351351315")]
    [InlineData("539843191(69)")]
    public void InvalidCep(string invalidPhoneNumber)
    {
        //Arrange / Act
        var phoneNumber = new PhoneNumber(invalidPhoneNumber);

        //Assert
        phoneNumber.IsValid.Should().BeFalse();
        phoneNumber.Number.Should().Be(invalidPhoneNumber);
    }

    [Theory(DisplayName = "If Phone Number Is Valid, Should Set As Valid")]
    [InlineData("(53)98431-9169")]
    [InlineData("(69) 33857411")]
    [InlineData("6737011926")]
    [InlineData("(96) 3552-5834")]
    public void ValidPhoneNumber(string validPhoneNumber)
    {
        //Arrange / Act
        var phoneNumber = new PhoneNumber(validPhoneNumber);

        //Assert
        phoneNumber.IsValid.Should().BeTrue();
        phoneNumber.Number.Should().Be(validPhoneNumber);
    }
}