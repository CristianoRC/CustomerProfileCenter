using CustomerProfileCenter.Domain.ValueObjects.Documents;

namespace CustomerProfileCenter.Domain.Entities;

public class Company : Customer
{
    public Company(string name, string corporateName, IDocument document) : base(name, document)
    {
        CorporateName = corporateName;
    }

    public string CorporateName { get; }
}