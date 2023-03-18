using CustomerProfileCenter.Domain.ValueObjects.Documents;

namespace CustomerProfileCenter.Infra.Data.HashAndCryptography;

public interface IDocumentSecurityService
{
    string GetDocumentHash(IDocument document);
    string EncryptDocument(IDocument document);
    string DecryptDocument(IDocument document);
}