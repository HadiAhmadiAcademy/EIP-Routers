using MassTransit;
using Messages.PurchaseOrders;

namespace Consumer2.Handlers;

public class PlaceOrderHandler : IdempotentHandler<PlaceOrder>
{
    protected override Task Process(PlaceOrder message)
    {
        Console.WriteLine("Message Received.");
        Console.WriteLine($"Message Id : {message.MessageId}");
        Console.WriteLine("---------------------");
        return Task.CompletedTask;
    }
}