using Messages.Core;

namespace Messages.PurchaseOrders;

public class CompletedOrder : IMessage
{
    public long OrderNumber { get; set; }
    public long VendorId { get; set; }
    public Guid MessageId { get; set; }
    public List<OrderLine> OrderLines { get; set; } = new();
    public ProcessInfo? ProcessInfo { get; set; }
}