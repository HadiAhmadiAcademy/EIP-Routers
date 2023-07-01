using System.Text.Json.Serialization;
using MassTransit;
using Messages.PurchaseOrders;
using Newtonsoft.Json;

namespace Consumer1.Handlers;

public class OrderHandler : IConsumer<CompletedOrder>
{
    public Task Consume(ConsumeContext<CompletedOrder> context)
    {
        Console.WriteLine("Message Received - CompletedOrder : ");
        Console.WriteLine("---------------------");
        var json = JsonConvert.SerializeObject(context.Message, Formatting.Indented);
        Console.WriteLine(json);
        Console.WriteLine("---------------------");
        return Task.CompletedTask;
    }
}