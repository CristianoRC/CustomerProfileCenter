using NetDevPack.Brasil.Documentos.Validacao;

namespace CustomerProfileCenter.Domain.ValueObjects.Documents;

public record Cnpj : IDocument
{
    public Cnpj(string number)
    {
        var documentNumberWithoutEspecialCharacters = RemoveEspecialCharacters(number);
        var validator = new CnpjValidador(documentNumberWithoutEspecialCharacters);
        IsValid = validator.EstaValido();
        Number = documentNumberWithoutEspecialCharacters;
    }

    private static string RemoveEspecialCharacters(string number)
    {
        return number.Replace("-", string.Empty).Replace(".", string.Empty)
            .Replace("/", string.Empty).Trim();
    }

    public string Number { get; }
    public bool IsValid { get; }
}