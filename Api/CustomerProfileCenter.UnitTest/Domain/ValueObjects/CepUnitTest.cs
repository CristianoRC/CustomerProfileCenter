using CustomerProfileCenter.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CustomerProfileCenter.UnitTest.Domain.ValueObjects;

public class CepUnitTest : BaseTest
{
    [Theory(DisplayName = "If Cep Number Is Invalid, Should Set As Invalid")]
    [InlineData("123458644")]
    [InlineData("1asdf44")]
    [InlineData("asdfghhjk")]
    public void InvalidCep(string invalidCepNumber)
    {
        //Arrange / Act
        var cep = new Cep(invalidCepNumber);

        //Assert
        cep.IsValid.Should().BeFalse();
    }

    [Theory(DisplayName = "If Cep Number Is Valid, Should Set As Valid")]
    [InlineData("96085-000")]
    [InlineData("69075-771")]
    [InlineData("39086382")]
    public void ValidCpfNumber(string validCepNumber)
    {
        //Arrange / Act
        var cep = new Cep(validCepNumber);

        //Assert
        cep.IsValid.Should().BeTrue();
    }

    [Theory(DisplayName = "Should Always Remove Special Characters From Cep")]
    [InlineData("96085-000")]
    [InlineData("69075-771")]
    [InlineData("39086382")]
    [InlineData("1asd-f44")]
    [InlineData("asd-fg-hhjk")]
    public void RemoveSpecialCharacters(string validCepNumber)
    {
        //Arrange / Act
        var cep = new Cep(validCepNumber);

        //Assert
        cep.Number.Should().NotContain("-");
    }
}