using Faker;
using MassTransit;
using Messages.PurchaseOrders;

namespace Consumer2.Handlers;

public class PlaceOrderHandler : IConsumer<PlaceOrder>
{
    private static long _totalReceived = 0;

    public Task Consume(ConsumeContext<PlaceOrder> context)
    {
        Interlocked.Increment(ref _totalReceived);
        Thread.Sleep(RandomNumber.Next(500, 3000));

        Console.WriteLine($"Message Received. OrderNo: {context.Message.OrderNumber}");
        Console.WriteLine($"-------------------| Total Received: {_totalReceived} |------------");
        return Task.CompletedTask;
    }
}