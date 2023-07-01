namespace Messages.PurchaseOrders;

public class OrderLine
{
    public long ProductId { get; set; }
    public long Quantity { get; set; }
    public long Price { get; set; }
}