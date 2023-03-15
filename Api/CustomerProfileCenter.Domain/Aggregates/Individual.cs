using CustomerProfileCenter.Domain.ValueObjects.Documents;

namespace CustomerProfileCenter.Domain.Aggregates;

public class Individual : Customer
{
    public Individual(string name, IDocument document, DateOnly birthDay) : base(name, document)
    {
        BirthDay = birthDay;
    }

    public DateOnly BirthDay { get; }
}