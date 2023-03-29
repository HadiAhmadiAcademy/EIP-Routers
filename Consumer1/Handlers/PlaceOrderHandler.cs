using System.Text.Json.Serialization;
using MassTransit;
using Messages.PurchaseOrders;
using Newtonsoft.Json;

namespace Consumer1.Handlers;

public class PlaceOrderHandler : IConsumer<PlaceOrder>
{
    public Task Consume(ConsumeContext<PlaceOrder> context)
    {
        Console.WriteLine("Message Received : ");
        Console.WriteLine("---------------------");
        var json = JsonConvert.SerializeObject(context.Message, Formatting.Indented);
        Console.WriteLine(json);
        Console.WriteLine("---------------------");
        return Task.CompletedTask;
    }
}