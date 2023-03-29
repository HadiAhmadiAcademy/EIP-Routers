using MassTransit;
using Messages.PurchaseOrders;
using Router.Splitters.Iterating;

namespace Router.Handlers;

public class PlaceOrderHandler : IConsumer<PlaceOrder>
{
    private static readonly IteratingSplitter<PlaceOrder, PlaceOrder> _splitter;
    static PlaceOrderHandler()
    {
        _splitter = CreateIteratingSplitter.Which()
            .TakesInput<PlaceOrder>()
            .Produces<PlaceOrder>()
            .SplitBasedOn(a => a.OrderLines.Count)
            .UsingConverter((input, index) => new PlaceOrder()
            {
                VendorId = input.VendorId,
                IssueDate = input.IssueDate,
                OrderNumber = input.OrderNumber,
                OrderLines = new List<OrderLine> { input.OrderLines[index] }
            })
            .Build();
    }


    public async Task Consume(ConsumeContext<PlaceOrder> context)
    {
        var messageSegments = _splitter.Split(context.Message);

        foreach (var segment in messageSegments)
            await context.Send(new Uri("queue:Consumer1"), segment);
    }
}