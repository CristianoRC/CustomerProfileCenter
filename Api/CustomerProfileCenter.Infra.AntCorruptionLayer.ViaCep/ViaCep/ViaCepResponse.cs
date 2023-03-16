using CustomerProfileCenter.Domain.Entities;
using CustomerProfileCenter.Domain.ValueObjects;

namespace CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;

public record ViaCepResponse
{
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string Uf { get; set; }
    public string Ibge { get; set; }
    public string Gia { get; set; }
    public string Ddd { get; set; }
    public string Siafi { get; set; }

    public Address ToDomainAddress()
    {
        return new Address(new Cep(Cep), Localidade, Logradouro, Bairro, Uf, string.Empty, string.Empty);
    }
}