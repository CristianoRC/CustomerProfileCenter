using CustomerProfileCenter.Domain.ValueObjects.Documents;

namespace CustomerProfileCenter.Domain.Entities;

public class Individual : Customer
{
    public Individual(string name, IDocument document, DateOnly birthDay) : base(name, document)
    {
        BirthDay = birthDay;
    }

    public DateOnly BirthDay { get; }
}