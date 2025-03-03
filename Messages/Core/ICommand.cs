namespace Messages.Core;

public interface IMessage
{
    public Guid MessageId { get; set; }
}

public interface ICommand : IMessage
{
}