using Refit;

namespace CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;

public interface IViaCepClient
{
    [Get("/{cep}/json")]
    Task<ViaCepResponse> GetCepInformation(string cep);
}