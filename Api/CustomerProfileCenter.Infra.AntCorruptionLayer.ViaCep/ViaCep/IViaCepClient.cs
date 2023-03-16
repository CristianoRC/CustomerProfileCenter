using Refit;

namespace CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;

internal interface IViaCepClient
{
    [Get("/{cep}/json")]
    Task<ViaCepResponse> GetCepInformation(string cep);
}