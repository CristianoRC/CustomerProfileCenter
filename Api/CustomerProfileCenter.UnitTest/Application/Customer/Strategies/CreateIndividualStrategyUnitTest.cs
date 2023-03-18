using Bogus.Extensions.Brazil;
using CustomerProfileCenter.Application.Address;
using CustomerProfileCenter.Application.Customer;
using CustomerProfileCenter.Application.Customer.Strategies;
using CustomerProfileCenter.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace CustomerProfileCenter.UnitTest.Application.Customer.Strategies;

public class CreateIndividualStrategyUnitTest : BaseTest
{
    [Fact(DisplayName = "Should Return Error If CPF Is Invalid")]
    public async Task ReturnErrorIfCpfIsInvalid()
    {
        //Arrange
        var customerRepository = Mock.Of<ICustomerRepository>();
        var addressService = Mock.Of<IAddressService>();
        var createIndividualStrategy = new CreateIndividualStrategy(customerRepository, addressService);

        var createCustomerCommand = new CreateCustomerCommand()
        {
            Name = Faker.Person.FullName,
            Document = new CustomerDocument("41894", EDocumentType.Cpf),
            Birthday = Faker.Person.DateOfBirth
        };
        //Act
        var createIndividualResponse = await createIndividualStrategy.CreateCustomer(createCustomerCommand);

        //Assert
        createIndividualResponse.HasError.Should().BeTrue();
        createIndividualResponse.ErrorMessage.Should().Be("CPF Inválido.");
    }

    [Fact(DisplayName = "Should Return Error If Name Is Null Or Empty")]
    public async Task ReturnErrorNameNullOrEmpty()
    {
        //Arrange
        var customerRepository = Mock.Of<ICustomerRepository>();
        var addressService = Mock.Of<IAddressService>();
        var createIndividualStrategy = new CreateIndividualStrategy(customerRepository, addressService);
        var createCustomerCommand = new CreateCustomerCommand()
        {
            Name = Faker.PickRandom(string.Empty, null)!,
            Document = new CustomerDocument(Faker.Person.Cpf(), EDocumentType.Cpf),
            Birthday = Faker.Person.DateOfBirth
        };
        //Act
        var createIndividualResponse = await createIndividualStrategy.CreateCustomer(createCustomerCommand);

        //Assert
        createIndividualResponse.HasError.Should().BeTrue();
        createIndividualResponse.ErrorMessage.Should().Be("Nome Obrigatório.");
    }
}