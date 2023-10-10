using CommandLine;
using DequeNet;
using Distributor.Handlers;
using MassTransit;
using Messages.Auctions;
using Microsoft.Extensions.Options;

namespace Router
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("--------------Distributor------");

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
                sbc.ReceiveEndpoint("Distributor", ep =>
                {
                    ep.ConcurrentMessageLimit = 1;
                    ep.UseMessageRetry(r => r.Immediate(5));
                    ep.Consumer<BidPlacedHandler>();
                });
            });
            await bus.StartAsync();
            Console.WriteLine("Bus is running. Waiting for messages...");
            Console.ReadLine();
        }
    }

}