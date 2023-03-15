using CustomerProfileCenter.Domain.Entities;
using CustomerProfileCenter.Domain.ValueObjects;
using CustomerProfileCenter.Domain.ValueObjects.Documents;

namespace CustomerProfileCenter.Domain.Aggregates;

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
}