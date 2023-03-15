namespace CustomerProfileCenter.Domain.ValueObjects.Documents;

public interface IDocument
{
    string Number { get; }
    bool IsValid { get; }
}