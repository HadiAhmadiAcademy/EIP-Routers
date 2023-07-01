using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages.Core;
using Messages.Core.DataPartitioning;

namespace Messages.PurchaseOrders
{
    public class PlaceOrder : ICommand, IHaveCorrelationId<long>
    {
        public long OrderNumber { get; set; }
        public long VendorId { get; set; }
        public Guid MessageId { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public SliceInfo SliceInfo { get; set; }
        public long CorrelationId => OrderNumber;
    }
}
