namespace Messages.Core;

public interface IMessage
{
    public Guid MessageId { get; }
}

public interface ICommand : IMessage
{
}