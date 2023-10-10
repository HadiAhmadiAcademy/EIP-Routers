using Distributor.Model;
using Distributor.Model.RouterBuilder;
using MassTransit;
using Messages.Auctions;

namespace Distributor.Handlers;

public class BidPlacedHandler : IConsumer<BidPlaced>
{
    private static IContentBasedRouter<BidPlaced> Router;
    static BidPlacedHandler()
    {
        Router = UseContentBasedRouter.For<BidPlaced>()
            .When(a => a.OrderId % 2 == 0).RouteTo("queue:Resequencer1")
            .When(a => a.OrderId % 2 == 1).RouteTo("queue:Resequencer2")
            .Build();
    }
    public Task Consume(ConsumeContext<BidPlaced> context)
    {
        var destination = Router.FindDestinationFor(context.Message);
        Console.WriteLine($"Message Received. Routing the message to : {destination}");
        return context.Send(new Uri(destination), context.Message);
    }
}