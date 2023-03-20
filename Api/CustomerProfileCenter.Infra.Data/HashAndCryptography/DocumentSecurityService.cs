using System.Security.Cryptography;
using System.Text;
using CustomerProfileCenter.Domain.ValueObjects.Documents;
using Microsoft.Extensions.Configuration;

namespace CustomerProfileCenter.Infra.Data.HashAndCryptography;

public class DocumentSecurityService : IDocumentSecurityService
{
    private readonly string _saltKey;
    private readonly byte[] _key;
    private readonly byte[] _initializationVector;

    public DocumentSecurityService(IConfiguration configuration)
    {
        _key = Convert.FromBase64String(configuration["Key"]);
        _initializationVector = Convert.FromBase64String(configuration["Iv"]);
        _saltKey = configuration["SaltKey"];
    }

    public string GetDocumentHash(IDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);

        var hmacSha256 = new HMACSHA256(Encoding.UTF8.GetBytes(_saltKey));
        var hmac = hmacSha256.ComputeHash(Encoding.UTF8.GetBytes(document.Number));
        return BitConverter.ToString(hmac).Replace("-", string.Empty);
    }

    private Aes GetAes()
    {
        var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _initializationVector;
        return aes;
    }

    public string EncryptDocument(IDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        var messageBytes = Encoding.UTF8.GetBytes(document.Number);
        var aes = GetAes();
        var encryptedMessage = aes.EncryptCbc(messageBytes, _initializationVector, PaddingMode.PKCS7);
        return Convert.ToBase64String(encryptedMessage);
    }

    public string DecryptDocument(IDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        var messageBytes = Convert.FromBase64String(document.Number);
        var aes = GetAes();
        var decryptedMessage = aes.DecryptCbc(messageBytes, _initializationVector, PaddingMode.PKCS7);
        return Encoding.UTF8.GetString(decryptedMessage);
    }
}