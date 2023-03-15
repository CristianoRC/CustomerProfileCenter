using NetDevPack.Brasil.Localizacao.Validacao;

namespace CustomerProfileCenter.Domain.ValueObjects;

public record Cep
{
    public Cep(string number)
    {
        var cepWithoutSpecialCharacters = RemoveEspecialCharacters(number);
        var validator = new CepValidador(cepWithoutSpecialCharacters);
        IsValid = validator.EstaValido();
        Number = cepWithoutSpecialCharacters;
    }

    private static string RemoveEspecialCharacters(string number)
    {
        return number.Replace("-", string.Empty).Trim();
    }

    public string Number { get; }
    public bool IsValid { get; }
}