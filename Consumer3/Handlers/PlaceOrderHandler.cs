using System.Security.Cryptography;
using Faker;
using MassTransit;
using Messages.PurchaseOrders;

namespace Consumer3.Handlers;

public class PlaceOrderHandler : IConsumer<PlaceOrder>
{
    static object lockObj = new object();
    private static long _totalReceived = 0;

    public Task Consume(ConsumeContext<PlaceOrder> context)
    {
        Thread.Sleep(RandomNumber.Next(500, 3000));
        Console.WriteLine($"Message Received. OrderNo: {context.Message.OrderNumber}");

        lock (lockObj)
        {
            _totalReceived++;
            Console.WriteLine($"-------------------| Total Received: {_totalReceived} |------------");
        }
        return Task.CompletedTask;
    }
}