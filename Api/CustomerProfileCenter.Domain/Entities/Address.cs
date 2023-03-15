using CustomerProfileCenter.Domain.ValueObjects;

namespace CustomerProfileCenter.Domain.Entities;

public class Address
{
    public Address(Cep cep, string street, string neighborhood, string uf, string number, string? complement)
    {
        Cep = cep;
        Street = street;
        Neighborhood = neighborhood;
        UF = uf;
        Number = number;
        Complement = complement;
    }

    public Cep Cep { get; }
    public string Street { get; }
    public string Neighborhood { get; }
    public string UF { get; }
    public string Number { get; }
    public string? Complement { get; }
}