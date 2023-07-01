using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using MassTransit;
using Messages.PurchaseOrders;
using Newtonsoft.Json;

namespace Router.Handlers;

public class PlaceOrderHandler : IConsumer<PlaceOrder>
{
    private static OrderAggregator Aggregator = new();

    public async Task Consume(ConsumeContext<PlaceOrder> context)
    {
        Console.WriteLine("Message Received...");

        var aggregatedResult = await Aggregator.Aggregate(context.Message);
        if (aggregatedResult.HasValue)
        {
            Console.WriteLine("--------------------Aggregation Completed-----------------------");
            Console.WriteLine(JsonConvert.SerializeObject(aggregatedResult.Value, Formatting.Indented));
            Console.WriteLine("----------------------------------------------------------------");
            var endpoint = await context.GetSendEndpoint(new Uri("queue:Consumer1"));
            await endpoint.Send(aggregatedResult.Value);
            Console.WriteLine("Sent to output channel...");
            Console.WriteLine("----------------------------------------------------------------");
        }
        else
        {
            Console.WriteLine("Aggregation not completed. waiting for more messages...");
        }
    }
}