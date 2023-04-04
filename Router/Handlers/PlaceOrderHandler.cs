using MassTransit;
using Messages.PurchaseOrders;
using Router.Model;
using Router.Model.Extensions;
using Router.Model.RouterBuilder;

namespace Router.Handlers;

public class PlaceOrderHandler : IConsumer<PlaceOrder>
{
    private static readonly IRecipientList<PlaceOrder> _router;
    static PlaceOrderHandler()
    {
        _router = UseRecipientList.For<PlaceOrder>()
            .When(a => TotalPriceOfItems(a).IsInRangeOf(1,10000)).RouteTo("queue:Consumer1", "queue:Consumer2")
            .When(a => TotalPriceOfItems(a).IsInRangeOf(10001, 20000)).RouteTo("queue:Consumer2", "queue:Consumer3")
            .WhenNoCriteriaMatchesRouteTo("queue:Consumer3")
            .Build();
    }

    public async Task Consume(ConsumeContext<PlaceOrder> context)
    {
        Console.WriteLine($"Message Received. Total Price of Items is : {TotalPriceOfItems(context.Message)}");

        var destinations = _router.FindDestinationsFor(context.Message);
        foreach (var destination in destinations)
        {
            Console.WriteLine($"Routing Message to : {destination}");
            await context.Send(new Uri(destination), context.Message);
        }

        Console.WriteLine("-----------------------");
    }
    private static decimal TotalPriceOfItems(PlaceOrder order)
    {
        return order.OrderLines.Select(a => a.PricePerUnit * a.Quantity).Sum();
    }
}