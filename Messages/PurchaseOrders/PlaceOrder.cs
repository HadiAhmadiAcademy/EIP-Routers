using Messages.Core;

namespace Messages.PurchaseOrders
{
    public class PlaceOrder : ICommand, ICloneable
    {
        public long OrderNumber { get; set; }
        public long VendorId { get; set; }
        public DateTime IssueDate { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public Guid MessageId { get; } = Guid.NewGuid();
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}