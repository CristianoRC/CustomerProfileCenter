using CustomerProfileCenter.Domain.ValueObjects;

namespace CustomerProfileCenter.Domain.Entities;

public class Address
{
    public Address(Cep cep, string number, string? complement)
    {
        Cep = cep;
        Number = number;
        Complement = complement;
    }

    public Cep Cep { get; }
    public string Number { get; }
    public string? Complement { get; }
}