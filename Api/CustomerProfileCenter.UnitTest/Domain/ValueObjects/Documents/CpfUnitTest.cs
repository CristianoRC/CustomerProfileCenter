using Bogus.Extensions.Brazil;
using CustomerProfileCenter.Domain.ValueObjects.Documents;
using FluentAssertions;
using Xunit;

namespace CustomerProfileCenter.UnitTest.Domain.ValueObjects.Documents;

public class CpfUnitTest : BaseTest
{
    [Theory(DisplayName = "If Document Number Is Invalid, Should Set Status As Invalid")]
    [InlineData("")]
    [InlineData("12348")]
    [InlineData("as15841358")]
    public void InvalidCpfNumberStatus(string cpfNumber)
    {
        //Arrange - Act
        var cpf = new Cpf(cpfNumber);

        //Assert
        cpf.IsValid.Should().BeFalse();
    }

    [Theory(DisplayName = "If Document Number Is Invalid, Should Always Set The Value")]
    [InlineData("")]
    [InlineData("12348")]
    [InlineData("as15841358")]
    public void InvalidCpfNumber(string cpfNumber)
    {
        //Arrange - Act
        var cpf = new Cpf(cpfNumber);

        //Assert
        cpf.Number.Should().Be(cpfNumber);
    }

    [Fact(DisplayName = "Should Remove Special Characters To CPF Number")]
    public void RemoveSpecialCharacters()
    {
        //Arrange
        var validCpfNumberWithSpecialCharacters = Faker.Person.Cpf();

        //Act
        var cpf = new Cpf(validCpfNumberWithSpecialCharacters);

        //Assert
        cpf.Number.Should().NotContain("-");
        cpf.Number.Should().NotContain(".");
    }

    [Fact(DisplayName = "If Document Number is Valid, Should Set Status As Valid")]
    public void ValidDocumentStatus()
    {
        //Arrange
        var validCpfNumberWithSpecialCharacters = Faker.Person.Cpf();

        //Act
        var cpf = new Cpf(validCpfNumberWithSpecialCharacters);

        //Assert
        cpf.IsValid.Should().BeTrue();
    }
}