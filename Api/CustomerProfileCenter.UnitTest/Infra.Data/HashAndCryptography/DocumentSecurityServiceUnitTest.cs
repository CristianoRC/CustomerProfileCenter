using System.Security.Cryptography;
using Bogus.Extensions.Brazil;
using CustomerProfileCenter.Application.Customer;
using CustomerProfileCenter.Domain.ValueObjects.Documents;
using CustomerProfileCenter.Infra.Data.HashAndCryptography;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CustomerProfileCenter.UnitTest.Infra.Data.HashAndCryptography;

public class DocumentSecurityServiceUnitTest : BaseTest
{
    private IDocument GenerateRandomDocument()
    {
        var documentType = Faker.PickRandom(EDocumentType.Cnpj, EDocumentType.Cpf);

        if (documentType == EDocumentType.Cpf)
            return new Cpf(Faker.Person.Cpf());
        return new Cnpj(Faker.Company.Cnpj());
    }

    private static IConfigurationRoot GetConfigurationMock()
    {
        RandomNumberGenerator random = RandomNumberGenerator.Create();
        byte[] key = new byte[16];
        byte[] iv = new byte[16];
        random.GetBytes(key);
        random.GetBytes(iv);

        var myConfiguration = new Dictionary<string, string>
        {
            {"SaltKey", "D*Ezu1hl612."},
            {"EncryptKey", "..."},
            {"Iv", Convert.ToBase64String(iv)},
            {"Key", Convert.ToBase64String(key)}
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();
    }

    [Fact(DisplayName = "On GetDocumentHash, Should Return a SHA-256 Of Input Document")]
    public void GetDocumentHash()
    {
        //Arrange
        const string documentNumber = "848.471.210-90";
        const string documentHashSha256WithSaltKey = "461652D38E5CCA6796962D177A8FEBE86062866A36CEC0D177B126E419859C1A";
        var document = new Cpf(documentNumber);
        var documentSecurityService = new DocumentSecurityService(GetConfigurationMock());

        //Act
        var documentHashResponse = documentSecurityService.GetDocumentHash(document);

        //Assert
        documentHashResponse.Should().Be(documentHashSha256WithSaltKey);
    }

    [Fact(DisplayName = "On GetDocumentHash, Should Throw Error If Document Is Null")]
    public void GetDocumentHashNull()
    {
        //Arrange
        var documentSecurityService = new DocumentSecurityService(GetConfigurationMock());
        IDocument document = null;

        //Act
        var onGetDocumentHash = () => documentSecurityService.GetDocumentHash(document!);

        //Assert
        onGetDocumentHash.Should().Throw<ArgumentNullException>();
    }

    [Fact(DisplayName = "Should Encrypt Document With Success")]
    public void EncryptDocumentWithSuccess()
    {
        //Arrange
        var documentSecurityService = new DocumentSecurityService(GetConfigurationMock());
        var document = GenerateRandomDocument();

        //Act
        var encryptedDocument = documentSecurityService.EncryptDocument(document!);

        //Assert
        encryptedDocument.Should().NotBeEmpty();
        encryptedDocument.Should().NotBeNull();
        encryptedDocument.Should().NotBeEquivalentTo(document.Number);
    }

    [Fact(DisplayName = "On Encrypt different Documents, Should Not Return The Same Value")]
    public void OnEncryptDocumentDifferentValues()
    {
        //Arrange
        var documentSecurityService = new DocumentSecurityService(GetConfigurationMock());
        var firstDocument = GenerateRandomDocument();
        var secondDocument = GenerateRandomDocument();

        //Act
        var firstDocumentEncrypted = documentSecurityService.EncryptDocument(firstDocument);
        var secondDocumentEncrypted = documentSecurityService.EncryptDocument(secondDocument);

        //Assert
        firstDocumentEncrypted.Should().NotBeEquivalentTo(secondDocumentEncrypted);
    }

    [Fact(DisplayName = "On Encrypt Documents, Should Always Return The Same Value")]
    public void OnEncryptDocumentAlwaysTheSameValue()
    {
        //Arrange
        var documentSecurityService = new DocumentSecurityService(GetConfigurationMock());
        var document = GenerateRandomDocument();

        //Act
        var documentEncrypted = documentSecurityService.EncryptDocument(document);
        var documenEncryptedSecondTime = documentSecurityService.EncryptDocument(document);

        //Assert
        documentEncrypted.Should().Be(documenEncryptedSecondTime);
    }

    [Fact(DisplayName = "On Encrypt Document, Should Throw Error If Document Is Null")]
    public void NullDocumentOnEncrypt()
    {
        //Arrange
        var documentSecurityService = new DocumentSecurityService(GetConfigurationMock());
        IDocument document = null;

        //Act
        var onEncryptDocument = () => documentSecurityService.EncryptDocument(document!);

        //Assert
        onEncryptDocument.Should().Throw<ArgumentNullException>();
    }

    [Fact(DisplayName = "Should Be Possible Encrypt And Decrypt With Success")]
    public void EncryptAndDecrypt()
    {
        //Arrange
        var documentSecurityService = new DocumentSecurityService(GetConfigurationMock());
        var document = new Cpf("136.034.800-07");

        //Act
        var encryptedDocument = documentSecurityService.EncryptDocument(document!);
        var decryptedDocument = documentSecurityService.DecryptDocument(new Cpf(encryptedDocument));

        //Assert
        decryptedDocument.Should().Be(document.Number);
        encryptedDocument.Should().NotBeEquivalentTo(decryptedDocument);
    }

    [Fact(DisplayName = "On Decrypt Document, Should Throw Error If Document Is Null")]
    public void NullDocumentOnDecrypt()
    {
        //Arrange
        var documentSecurityService = new DocumentSecurityService(GetConfigurationMock());
        IDocument document = null;

        //Act
        var onDecryptDocument = () => documentSecurityService.DecryptDocument(document!);

        //Assert
        onDecryptDocument.Should().Throw<ArgumentNullException>();
    }
}