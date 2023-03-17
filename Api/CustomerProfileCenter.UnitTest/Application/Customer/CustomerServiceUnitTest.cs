using Bogus.Extensions.Brazil;
using CustomerProfileCenter.Application.Customer;
using CustomerProfileCenter.Application.Customer.Strategies;
using CustomerProfileCenter.Application.MessageBus;
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects.Documents;
using FluentAssertions;
using Moq;
using Xunit;

namespace CustomerProfileCenter.UnitTest.Application.Customer;

public class CustomerServiceUnitTest : BaseTest
{
    [Fact(DisplayName = "On Enqueue Create Command, Should Not Process If Document Has Already Registered")]
    public async Task EnqueueCreateCommand()
    {
        //Arrange
        var documentType = Faker.PickRandom(EDocumentType.Cnpj, EDocumentType.Cpf);
        var documentNumber = documentType == EDocumentType.Cnpj ? Faker.Company.Cnpj() : Faker.Person.Cpf();

        var customerRepository = new Mock<ICustomerRepository>();
        customerRepository.Setup(x => x.CustomerAlreadyRegistered(It.IsAny<IDocument>()))
            .ReturnsAsync(true);
        var service = new CustomerService(customerRepository.Object, Enumerable.Empty<ICreateCustomerStrategy>(),
            Mock.Of<ICustomerRegisterBus>());

        var command = new CreateCustomerCommand()
        {
            DocumentType = documentType,
            DocumentNumber = documentNumber
        };

        //Act
        var response = await service.EnqueueCreateCustomerCommand(command);

        //Assert
        response.HasError.Should().BeTrue();
        response.ErrorMessage.Should().Be("Cliente já cadastrado");
    }

    [Fact(DisplayName = "On Enqueue Create Command, Should Send To Bus If Document Is Valid")]
    public async Task SendMessageToBus()
    {
        //Arrange
        var documentType = Faker.PickRandom(EDocumentType.Cnpj, EDocumentType.Cpf);
        var documentNumber = documentType == EDocumentType.Cnpj ? Faker.Company.Cnpj() : Faker.Person.Cpf();

        var customerRepository = new Mock<ICustomerRepository>();
        customerRepository.Setup(x => x.CustomerAlreadyRegistered(It.IsAny<IDocument>()))
            .ReturnsAsync(false);

        var messageBusMock = new Mock<ICustomerRegisterBus>();
        var service = new CustomerService(customerRepository.Object, Enumerable.Empty<ICreateCustomerStrategy>(),
            messageBusMock.Object);

        var command = new CreateCustomerCommand()
        {
            DocumentType = documentType,
            DocumentNumber = documentNumber
        };

        //Act
        var response = await service.EnqueueCreateCustomerCommand(command);

        //Assert
        messageBusMock.Verify(x => x.EnqueueCreateCustomerCommand(command), Times.Once);
        response.HasError.Should().BeFalse();
    }

    [Fact(DisplayName = "On Create, Should Not Process If Document Has Already Registered")]
    public async Task DocumentAlreadyProcess()
    {
        //Arrange
        var documentType = Faker.PickRandom(EDocumentType.Cnpj, EDocumentType.Cpf);
        var documentNumber = documentType == EDocumentType.Cnpj ? Faker.Company.Cnpj() : Faker.Person.Cpf();

        var customerRepository = new Mock<ICustomerRepository>();
        customerRepository.Setup(x => x.CustomerAlreadyRegistered(It.IsAny<IDocument>()))
            .ReturnsAsync(true);
        var service = new CustomerService(customerRepository.Object, Enumerable.Empty<ICreateCustomerStrategy>(),
            Mock.Of<ICustomerRegisterBus>());

        var command = new CreateCustomerCommand()
        {
            DocumentType = documentType,
            DocumentNumber = documentNumber
        };

        //Act
        var response = await service.CreateCustomer(command);

        //Assert
        response.HasError.Should().BeTrue();
        response.ErrorMessage.Should().Be("Cliente já cadastrado");
    }

    [Fact(DisplayName = "On Create, If User Is A Company Should Call The Company Strategy")]
    public async Task CompanyStrategy()
    {
        //Arrange
        var documentNumber = Faker.Random.AlphaNumeric(25);
        var customerRepository = new Mock<ICustomerRepository>();
        customerRepository.Setup(x => x.CustomerAlreadyRegistered(It.IsAny<IDocument>()))
            .ReturnsAsync(false);

        var individualStrategy = new Mock<ICreateCustomerStrategy>();
        individualStrategy.Setup(x => x.CustomerDocumentType)
            .Returns(EDocumentType.Cpf);

        var companyStrategy = new Mock<ICreateCustomerStrategy>();
        companyStrategy.Setup(x => x.CustomerDocumentType)
            .Returns(EDocumentType.Cnpj);

        var strategies = new[] {individualStrategy.Object, companyStrategy.Object};
        var service = new CustomerService(customerRepository.Object, strategies, Mock.Of<ICustomerRegisterBus>());

        var command = new CreateCustomerCommand()
        {
            DocumentType = EDocumentType.Cnpj,
            DocumentNumber = documentNumber
        };

        //Act
        await service.CreateCustomer(command);

        //Assert
        companyStrategy.Verify(x => x.CreateCustomer(command), Times.Once);
        individualStrategy.Verify(x => x.CreateCustomer(It.IsAny<CreateCustomerCommand>()), Times.Never);
    }

    [Fact(DisplayName = "On Create, If User Is A Person Should Call The Individual Strategy")]
    public async Task IndividualStrategy()
    {
        //Arrange
        var documentNumber = Faker.Random.AlphaNumeric(25);
        var customerRepository = new Mock<ICustomerRepository>();
        customerRepository.Setup(x => x.CustomerAlreadyRegistered(It.IsAny<IDocument>()))
            .ReturnsAsync(false);

        var individualStrategy = new Mock<ICreateCustomerStrategy>();
        individualStrategy.Setup(x => x.CustomerDocumentType)
            .Returns(EDocumentType.Cpf);

        var companyStrategy = new Mock<ICreateCustomerStrategy>();
        companyStrategy.Setup(x => x.CustomerDocumentType)
            .Returns(EDocumentType.Cnpj);

        var strategies = new[] {individualStrategy.Object, companyStrategy.Object};
        var service = new CustomerService(customerRepository.Object, strategies, Mock.Of<ICustomerRegisterBus>());

        var command = new CreateCustomerCommand()
        {
            DocumentType = EDocumentType.Cpf,
            DocumentNumber = documentNumber
        };

        //Act
        await service.CreateCustomer(command);

        //Assert
        individualStrategy.Verify(x => x.CreateCustomer(command), Times.Once);
        companyStrategy.Verify(x => x.CreateCustomer(It.IsAny<CreateCustomerCommand>()), Times.Never);
    }

    [Fact(DisplayName = "On Create, Should Throw Argument Out Of Range Exception If Has Not Strategy For This One")]
    public async Task StrategyNotFound()
    {
        //Arrange
        var documentNumber = Faker.Random.AlphaNumeric(25);
        var customerRepository = new Mock<ICustomerRepository>();
        customerRepository.Setup(x => x.CustomerAlreadyRegistered(It.IsAny<IDocument>()))
            .ReturnsAsync(false);
        var service = new CustomerService(customerRepository.Object, Enumerable.Empty<ICreateCustomerStrategy>(),
            Mock.Of<ICustomerRegisterBus>());

        var command = new CreateCustomerCommand()
        {
            DocumentType = Faker.PickRandom(EDocumentType.Cpf, EDocumentType.Cnpj),
            DocumentNumber = documentNumber
        };

        //Act
        var onCreateCustomer = async () => await service.CreateCustomer(command);

        //Assert
        await onCreateCustomer.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }
}