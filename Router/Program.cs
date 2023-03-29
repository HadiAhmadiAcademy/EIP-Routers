using DequeNet;
using MassTransit;
using Messages.PurchaseOrders;
using Router.Handlers;

namespace Router
{
    internal class Program
    {
        static async Task Main(string[] args)
        {


            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
                sbc.ReceiveEndpoint("Router", ep =>
                {
                    ep.UseMessageRetry(r => r.Immediate(5));
                    ep.Consumer<PlaceOrderHandler>();
                });
            });
            await bus.StartAsync();
            Console.WriteLine("Bus is running. Waiting for messages...");
            Console.ReadLine();
        }
    }
}