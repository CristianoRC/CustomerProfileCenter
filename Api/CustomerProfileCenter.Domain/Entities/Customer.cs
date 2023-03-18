using CustomerProfileCenter.Domain.ValueObjects;
using CustomerProfileCenter.Domain.ValueObjects.Documents;

namespace CustomerProfileCenter.Domain.Entities;

public abstract class Customer
{
    //TODO: Criar métodos para atualizar Email, Endereço e Telefone
    protected Customer(string name, IDocument document)
    {
        Name = name;
        Document = document;
    }

    public string Name { get; }
    public IDocument Document { get; }
    public Address? Address { get; protected set; }
    public EmailAddress? Email { get; protected set; }
    public PhoneNumber? PhoneNumber { get; set; }

    public bool NameIsValid => string.IsNullOrEmpty(Name) is false;
}