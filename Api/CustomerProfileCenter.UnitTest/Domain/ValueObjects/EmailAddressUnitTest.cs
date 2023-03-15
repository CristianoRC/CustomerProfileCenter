using CustomerProfileCenter.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CustomerProfileCenter.UnitTest.Domain.ValueObjects;

public class EmailAddressUnitTest : BaseTest
{
    [Theory(DisplayName = "If Address Is Invalid, Should Set Email As Invalid")]
    [InlineData("contato@cristianoprogramador")]
    [InlineData("contatocristianoprogramador.com.br")]
    public void InvalidEmail(string emailAddress)
    {
        //Arrange / Act
        var email = new EmailAddress(emailAddress);

        //Assert
        email.IsValid.Should().BeFalse();
        email.Address.Should().Be(emailAddress);
    }

    [Theory(DisplayName = "If Address Is Valid, Should Set Email As Valid")]
    [InlineData("contato@cristianoprogramador.com")]
    [InlineData("contato@cristianoprogramador.com.br")]
    public void ValidEmail(string emailAddress)
    {
        //Arrange / Act
        var email = new EmailAddress(emailAddress);

        //Assert
        email.IsValid.Should().BeTrue();
        email.Address.Should().Be(emailAddress);
    }
}