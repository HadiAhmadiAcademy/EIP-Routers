using MassTransit;
using Messages.PurchaseOrders;
using Router.Model;

namespace Router.Handlers;

public class PlaceOrderHandler : IConsumer<PlaceOrder>
{
    private readonly IContentBasedRouter<PlaceOrder> _router;
    public PlaceOrderHandler(IContentBasedRouter<PlaceOrder> router)
    {
        _router = router;
    }

    public Task Consume(ConsumeContext<PlaceOrder> context)
    {
        var destination = _router.FindDestinationFor(context.Message);
        Console.WriteLine($"Message Received. Routing the message to : {destination}");
        return context.Send(new Uri(destination), context.Message);
    }
}