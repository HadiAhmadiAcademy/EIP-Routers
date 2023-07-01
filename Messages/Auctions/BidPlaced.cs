using Messages.Core;

namespace Messages.Auctions;

public class BidPlaced : IEvent, IHaveSequenceId<long>
{
    public long BidAmount { get; set; }
    public Guid MessageId { get; set; }
    public DateTime PublishedDateTime { get; set; }
    public Guid EventId { get; set; }
    public long SequenceId { get; set; }
}