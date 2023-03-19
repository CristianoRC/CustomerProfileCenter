using Bogus.Extensions.Brazil;
using CustomerProfileCenter.Application.Address;
using CustomerProfileCenter.Application.Customer;
using CustomerProfileCenter.Application.Customer.Strategies;
using CustomerProfileCenter.Application.Response;
using CustomerProfileCenter.CrossCutting;
using CustomerProfileCenter.Domain.Entities;
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace CustomerProfileCenter.UnitTest.Application.Customer.Strategies;

public class CreateCompanyStrategyUnitTest : BaseTest
{
    [Fact(DisplayName = "Should Return Error If CNPJ Is Invalid")]
    public async Task ReturnErrorIfCnpjIsInvalid()
    {
        //Arrange
        var customerRepository = Mock.Of<ICustomerRepository>();
        var addressService = Mock.Of<IAddressService>();
        var createCompanyStrategy = new CreateCompanyStrategy(customerRepository, addressService);

        var createCustomerCommand = new CreateCustomerCommand()
        {
            Name = Faker.Person.FullName,
            Document = new CustomerDocument(Faker.Random.AlphaNumeric(13), EDocumentType.Cnpj),
        };
        //Act
        var createCompanyResponse = await createCompanyStrategy.CreateCustomer(createCustomerCommand);

        //Assert
        createCompanyResponse.HasError.Should().BeTrue();
        createCompanyResponse.ErrorMessage.Should().Be("CNPJ Inválido.");
    }

    [Fact(DisplayName = "Should Return Error If Name Is Null Or Empty")]
    public async Task ReturnErrorNameNullOrEmpty()
    {
        //Arrange
        var customerRepository = Mock.Of<ICustomerRepository>();
        var addressService = Mock.Of<IAddressService>();
        var createCompanyStrategy = new CreateCompanyStrategy(customerRepository, addressService);
        var createCustomerCommand = new CreateCustomerCommand()
        {
            Name = Faker.PickRandom(string.Empty, null)!,
            Document = new CustomerDocument(Faker.Company.Cnpj(), EDocumentType.Cnpj),
        };
        //Act
        var createCompanyResponse = await createCompanyStrategy.CreateCustomer(createCustomerCommand);

        //Assert
        createCompanyResponse.HasError.Should().BeTrue();
        createCompanyResponse.ErrorMessage.Should().Be("Nome Obrigatório.");
    }

    [Fact(DisplayName = "Should Save If Company Has Name And Document")]
    public async Task CreateCompanyWithSuccess()
    {
        //Arrange
        var customerRepository = new Mock<ICustomerRepository>();
        var addressService = Mock.Of<IAddressService>();
        var createCompanyStrategy = new CreateCompanyStrategy(customerRepository.Object, addressService);
        var createCustomerCommand = new CreateCustomerCommand()
        {
            Name = Faker.Person.FullName,
            Document = new CustomerDocument(Faker.Company.Cnpj(), EDocumentType.Cnpj),
        };
        //Act
        var createCompanyResponse = await createCompanyStrategy.CreateCustomer(createCustomerCommand);

        //Assert
        createCompanyResponse.HasError.Should().BeFalse();
        customerRepository.Verify(x => x.CreateCompany(It.IsAny<Company>(), It.IsAny<IIdempotentMessage>()),
            Times.Once);
    }

    [Fact(DisplayName = "Should Fill Address If Command Has A Valid Cep")]
    public async Task FillAddress()
    {
        //Arrange
        var address = new Address(new Cep("96085000"), Faker.Address.City(), Faker.Address.StreetName(),
            Faker.Address.StreetAddress(), "RS");
        var customerRepository = new Mock<ICustomerRepository>();
        var addressService = new Mock<IAddressService>();
        addressService.Setup(x => x.GetAddress("96085000"))
            .ReturnsAsync(new Response<Address>(address));

        var createCompanyStrategy = new CreateCompanyStrategy(customerRepository.Object, addressService.Object);
        var createCustomerCommand = new CreateCustomerCommand()
        {
            Name = Faker.Person.FullName,
            Document = new CustomerDocument(Faker.Company.Cnpj(), EDocumentType.Cnpj),
            Address = new CustomerAddress()
            {
                Number = "2886",
                Cep = "96085000",
                Complement = "n/a"
            }
        };

        var expectedAddress = address with {Number = "2886", Complement = "n/a"};

        //Act
        var createCompanyResponse = await createCompanyStrategy.CreateCustomer(createCustomerCommand);

        //Assert
        createCompanyResponse.HasError.Should().BeFalse();
        customerRepository.Verify(x => x.CreateCompany(It.Is<Company>(i => i.Address == expectedAddress),
            It.IsAny<IIdempotentMessage>()), Times.Once);
    }

    [Fact(DisplayName = "Should Keep Address If Command Has An Invalid Cep")]
    public async Task InvalidCep()
    {
        //Arrange
        var customerRepository = new Mock<ICustomerRepository>();
        var addressService = new Mock<IAddressService>();
        addressService.Setup(x => x.GetAddress("96085000"))
            .ReturnsAsync(new Response<Address>("CEP Não existe"));

        var createCompanyStrategy = new CreateCompanyStrategy(customerRepository.Object, addressService.Object);
        var createCustomerCommand = new CreateCustomerCommand()
        {
            Name = Faker.Person.FullName,
            Document = new CustomerDocument(Faker.Company.Cnpj(), EDocumentType.Cnpj),
            Address = new CustomerAddress()
            {
                Number = "2886",
                Cep = "96085000",
                Complement = "n/a"
            }
        };

        //Act
        var createCompanyResponse = await createCompanyStrategy.CreateCustomer(createCustomerCommand);

        //Assert
        createCompanyResponse.HasError.Should().BeFalse();
        customerRepository.Verify(x => x.CreateCompany(It.Is<Company>(i => i.Address == null),
            It.IsAny<IIdempotentMessage>()), Times.Once);
    }

    [Fact(DisplayName = "Should Fill Company E-mail Address If Command has e-mail Address")]
    public async Task FillEmailAddress()
    {
        //Arrange
        var customerRepository = new Mock<ICustomerRepository>();
        var addressService = new Mock<IAddressService>();

        var createCompanyStrategy = new CreateCompanyStrategy(customerRepository.Object, addressService.Object);
        var createCustomerCommand = new CreateCustomerCommand()
        {
            Name = Faker.Person.FullName,
            Document = new CustomerDocument(Faker.Company.Cnpj(), EDocumentType.Cnpj),
            EmailAddress = "contato@cristianoprogramador.com"
        };

        //Act
        var createCompanyResponse = await createCompanyStrategy.CreateCustomer(createCustomerCommand);

        //Assert
        createCompanyResponse.HasError.Should().BeFalse();
        customerRepository.Verify(x => x.CreateCompany(
            It.Is<Company>(i => i.Email.Address == "contato@cristianoprogramador.com"),
            It.IsAny<IIdempotentMessage>()), Times.Once);
    }

    [Fact(DisplayName = "Should Fill Company Phone Number If Has In The Command")]
    public async Task FillPhoneNumber()
    {
        //Arrange
        var customerRepository = new Mock<ICustomerRepository>();
        var addressService = new Mock<IAddressService>();

        var createCompanyStrategy = new CreateCompanyStrategy(customerRepository.Object, addressService.Object);
        var createCustomerCommand = new CreateCustomerCommand()
        {
            Name = Faker.Person.FullName,
            Document = new CustomerDocument(Faker.Company.Cnpj(), EDocumentType.Cnpj),
            EmailAddress = "contato@cristianoprogramador.com"
        };

        //Act
        var createCompanyResponse = await createCompanyStrategy.CreateCustomer(createCustomerCommand);

        //Assert
        createCompanyResponse.HasError.Should().BeFalse();
        customerRepository.Verify(x => x.CreateCompany(
            It.Is<Company>(i => i.Email.Address == "contato@cristianoprogramador.com"),
            It.IsAny<IIdempotentMessage>()), Times.Once);
    }
}