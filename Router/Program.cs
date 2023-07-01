using DequeNet;
using MassTransit;
using Messages.Auctions;
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
                    ep.ConcurrentMessageLimit = 1;
                    ep.UseMessageRetry(r => r.Immediate(5));
                    ep.Consumer<BidsResequencer>();
                });
            });
            await bus.StartAsync();
            Console.WriteLine("Bus is running. Waiting for messages...");
            Console.ReadLine();
        }
    }
}