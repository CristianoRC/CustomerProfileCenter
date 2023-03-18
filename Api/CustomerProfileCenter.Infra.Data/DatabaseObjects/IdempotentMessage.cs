using CustomerProfileCenter.CrossCutting;
using MongoDB.Bson;

namespace CustomerProfileCenter.Infra.Data.DatabaseObjects;

public class IdempotentMessage : IIdempotentMessage
{
    public IdempotentMessage(IIdempotentMessage idempotentMessage)
    {
        IdempotencyKey = idempotentMessage.IdempotencyKey;
        Id = new ObjectId();
    }
    
    public ObjectId Id { get; set; }
    public Guid IdempotencyKey { get; set; }
    public DateTime ProcessedDate => DateTime.Now;

    public void SetIdempotencyKey()
    {
        IdempotencyKey = Guid.NewGuid();
    }
}