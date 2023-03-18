namespace CustomerProfileCenter.Domain.ValueObjects;

public record Address
{
    public Address()
    {
    }

    public Address(Cep cep, string city, string street, string neighborhood, string uf)
    {
        Cep = cep;
        City = city;
        Street = street;
        Neighborhood = neighborhood;
        UF = uf;
        Number = string.Empty;
        Complement = string.Empty;
    }

    public Address(Cep cep, string city, string street, string neighborhood, string uf, string number,
        string complement)
    {
        Cep = cep;
        City = city;
        Street = street;
        Neighborhood = neighborhood;
        UF = uf;
        Number = number;
        Complement = complement;
    }

    public Cep Cep { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public string Neighborhood { get; init; }
    public string UF { get; init; }
    public string Number { get; init; }
    public string Complement { get; init; }
}