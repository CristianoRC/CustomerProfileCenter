using Bogus.Extensions.Brazil;
using CustomerProfileCenter.Domain.ValueObjects.Documents;
using FluentAssertions;
using Xunit;

namespace CustomerProfileCenter.UnitTest.Domain.ValueObjects.Documents;

public class CnpjUnitTest : BaseTest
{
    [Theory(DisplayName = "If Document Number Is Invalid, Should Set Status As Invalid")]
    [InlineData("")]
    [InlineData("12348")]
    [InlineData("as15841358")]
    public void InvalidCnpjNumberStatus(string cnpjNumber)
    {
        //Arrange - Act
        var cnpj = new Cnpj(cnpjNumber);

        //Assert
        cnpj.IsValid.Should().BeFalse();
    }

    [Theory(DisplayName = "If Document Number Is Invalid, Should Always Set The Value")]
    [InlineData("")]
    [InlineData("12348")]
    [InlineData("as15841358")]
    public void InvalidCnpjNumber(string cnpjNumber)
    {
        //Arrange - Act
        var cnpj = new Cnpj(cnpjNumber);

        //Assert
        cnpj.Number.Should().Be(cnpjNumber);
    }

    [Fact(DisplayName = "Should Remove Special Characters To CNPJ Number")]
    public void RemoveSpecialCharacters()
    {
        //Arrange
        var validCnpjNumberWithSpecialCharacters = Faker.Company.Cnpj();

        //Act
        var cnpj = new Cnpj(validCnpjNumberWithSpecialCharacters);

        //Assert
        cnpj.Number.Should().NotContain("-");
        cnpj.Number.Should().NotContain(".");
        cnpj.Number.Should().NotContain("/");
    }

    [Fact(DisplayName = "If Document Number is Valid, Should Set Status As Valid")]
    public void ValidDocumentStatus()
    {
        //Arrange
        var validCnpjNumberWithSpecialCharacters = Faker.Company.Cnpj();

        //Act
        var cnpj = new Cnpj(validCnpjNumberWithSpecialCharacters);

        //Assert
        cnpj.IsValid.Should().BeTrue();
    }
}