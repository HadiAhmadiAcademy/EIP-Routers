namespace Messages.Core;

public interface IHaveCorrelationId<T>
{
    public T CorrelationId { get; }
}