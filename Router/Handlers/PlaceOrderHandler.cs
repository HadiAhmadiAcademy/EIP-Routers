using MassTransit;
using Messages.PurchaseOrders;
using Router.Model;
using Router.Model.RouterBuilder;

namespace Router.Handlers;

public class PlaceOrderHandler : IConsumer<PlaceOrder>
{
    private static readonly IContentBasedRouter<PlaceOrder> _router;
    static PlaceOrderHandler()
    {
        _router =  UseContentBasedRouter.For<PlaceOrder>()
            .When(a => a.VendorId == 1).RouteTo("queue:Consumer1")
            .When(a => a.VendorId == 2).RouteTo("queue:Consumer2")
            .WhenNoCriteriaMatchesRouteTo("queue:Consumer3")
            .Build();
    }

    public Task Consume(ConsumeContext<PlaceOrder> context)
    {
        var destination = _router.FindDestinationFor(context.Message);
        Console.WriteLine($"Message Received. Routing the message to : {destination}");
        return context.Send(new Uri(destination), context.Message);
    }
}