namespace Messages.Core;

public interface IHaveSequenceId<T>
{
    public T SequenceId { get; }
}