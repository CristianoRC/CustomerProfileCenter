namespace CustomerProfileCenter.CrossCutting;

public interface IIdempotentMessage
{
    public Guid IdempotencyKey { get; }
    void SetIdempotencyKey();
}