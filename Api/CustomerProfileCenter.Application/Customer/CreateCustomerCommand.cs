using CustomerProfileCenter.Domain.ValueObjects.Documents;

namespace CustomerProfileCenter.Application.Customer;

public record CreateCustomerCommand
{
    public string Name { get; set; }
    public string DocumentNumber { get; set; }
    public EDocumentType DocumentType { get; set; }
    public CustomerAddress? Address { get; set; }
    public DateTime? Birthday { get; set; }
    public string CorporateName { get; set; }

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