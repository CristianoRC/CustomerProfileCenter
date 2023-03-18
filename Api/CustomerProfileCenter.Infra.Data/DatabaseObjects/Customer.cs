using CustomerProfileCenter.Application.Customer;
using MongoDB.Bson;

namespace CustomerProfileCenter.Infra.Data.DatabaseObjects;

public class Customer
{
    public Customer()
    {
        Id = new ObjectId();
    }

    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string DocumentNumber { get; set; }
    public string DocumentHash { get; set; }
    public EDocumentType DocumentType { get; set; }
    public DateTime? Birthday { get; set; }
    public string CorporateName { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public Address? Address { get; set; }
}

public record Address
{
    public Address()
    {
    }

    public Address(Domain.ValueObjects.Address address)
    {
        Cep = address.Cep.Number;
        Number = address.Number;
        Uf = address.UF;
        City = address.City;
        Street = address.Street;
        Complement = address.Complement;
        Neighborhood = address.Neighborhood;
    }

    public string Neighborhood { get; set; }

    public string Complement { get; set; }

    public string Street { get; set; }

    public string Number { get; set; }

    public string Uf { get; set; }

    public string City { get; set; }

    public string Cep { get; set; }
}