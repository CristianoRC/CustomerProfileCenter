using System.Text;
using CustomerProfileCenter.CrossCutting;
using CustomerProfileCenter.Domain.ValueObjects.Documents;

namespace CustomerProfileCenter.Application.Customer;

public record CreateCustomerCommand : IIdempotentMessage
{
    public Guid IdempotencyKey { get; private set; }

    public void SetIdempotencyKey()
    {
        IdempotencyKey = new Guid();
    }

    public string Name { get; set; }
    public string DocumentNumber { get; set; }
    public EDocumentType DocumentType { get; set; }
    public CustomerAddress? Address { get; set; }
    public DateTime? Birthday { get; set; }
    public string CorporateName { get; set; }

    public (bool hasError, string errorMessage) GetValidationErrors()
    {
        var errorsBuilder = new StringBuilder();

        if (string.IsNullOrEmpty(Name))
            errorsBuilder.AppendLine("Nome Obrigatório.");
        if (string.IsNullOrEmpty(DocumentNumber))
            errorsBuilder.AppendLine("Documento Obrigatório.");

        var errors = errorsBuilder.ToString();

        return (string.IsNullOrEmpty(errors) is false, errors);
    }

    public IDocument GetDocument()
    {
        return DocumentType == EDocumentType.Cnpj ? new Cnpj(DocumentNumber) : new Cpf(DocumentNumber);
    }
}

public record CustomerAddress
{
    public string Cep { get; set; }
    public string Number { get; set; }
    public string? Complement { get; set; }
}