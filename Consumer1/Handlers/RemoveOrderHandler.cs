using MassTransit;
using Messages.PurchaseOrders;

namespace Consumer1.Handlers;

public class RemoveOrderHandler : IConsumer<RemoveOrder>
{
    public Task Consume(ConsumeContext<RemoveOrder> context)
    {
        Console.WriteLine("Message Received.");
        Console.WriteLine("If Order exists, it will be deleted, else we assume it's already deleted");
        Console.WriteLine("---------------------");

        return Task.CompletedTask;
    }
}