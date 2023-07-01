namespace Messages.Core;

public interface IEvent : IMessage
{
    public DateTime PublishedDateTime { get; }
    public Guid EventId { get; }
}
