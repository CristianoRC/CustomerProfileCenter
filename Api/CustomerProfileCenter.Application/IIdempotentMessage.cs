namespace CustomerProfileCenter.Application;

public interface IIdempotentMessage
{
    public Guid IdempotencyKey { get; }
    void SetIdempotencyKey();
}