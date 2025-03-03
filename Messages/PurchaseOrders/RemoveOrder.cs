using Messages.Core;

namespace Messages.PurchaseOrders;

public class RemoveOrder : ICommand
{
    public Guid MessageId { get; set; }
    public long OrderId { get; set; }
}