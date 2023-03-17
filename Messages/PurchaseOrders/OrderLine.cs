namespace Messages.PurchaseOrders;

public class OrderLine
{
    public long ProductCode { get; set; }
    public decimal Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
}