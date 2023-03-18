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
    public Address? Address { get; private set; }
    public EmailAddress? Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }

    public void AddAddress(Address address)
    {
        Address = address;
    }

    public void AddPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber) is false)
            PhoneNumber = new PhoneNumber(phoneNumber);
    }

    public void AddEmailAddress(string emailAddress)
    {
        if (string.IsNullOrEmpty(emailAddress) is false)
            Email = new EmailAddress(emailAddress);
    }


    public bool NameIsValid => string.IsNullOrEmpty(Name) is false;
}