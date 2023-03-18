using System.Text;
using CustomerProfileCenter.CrossCutting;
using CustomerProfileCenter.Domain.ValueObjects.Documents;

namespace CustomerProfileCenter.Application.Customer;

public record CreateCustomerCommand : IIdempotentMessage
{
    public Guid IdempotencyKey { get; private set; }

    public void SetIdempotencyKey()
    {
        IdempotencyKey = Guid.NewGuid();
    }

    public string Name { get; set; }
    public CustomerDocument Document { get; set; }

    public CustomerAddress? Address { get; set; }
    public DateTime? Birthday { get; set; }
    public string CorporateName { get; set; }

    public (bool hasError, string errorMessage) GetValidationErrors()
    {
        var errorsBuilder = new StringBuilder();

        if (string.IsNullOrEmpty(Name))
            errorsBuilder.AppendLine("Nome Obrigatório.");
        if (string.IsNullOrEmpty(Document.Number))
            errorsBuilder.AppendLine("Documento Obrigatório.");

        var errors = errorsBuilder.ToString();

        return (string.IsNullOrEmpty(errors) is false, errors);
    }

    public IDocument GetDocument()
    {
        return Document.DocumentType == EDocumentType.Cnpj ? new Cnpj(Document.Number) : new Cpf(Document.Number);
    }
}

public record CustomerAddress
{
    public string Cep { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
}

public record CustomerDocument(string Number, EDocumentType DocumentType);