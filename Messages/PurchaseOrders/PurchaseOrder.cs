namespace Messages.PurchaseOrders
{
    public class PurchaseOrder
    {
        public long OrderNumber { get; set; }
        public long VendorId { get; set; }
        public DateTime IssueDate { get; set; }
        public OrderState State { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }
}