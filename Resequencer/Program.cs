using CommandLine;
using DequeNet;
using MassTransit;
using Messages.Auctions;
using Microsoft.Extensions.Options;
using Router.Core;
using Router.Handlers;

namespace Router
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var arguments = Parser.Default.ParseArguments<RouterArguments>(args).Value;
            Console.WriteLine($"Input Queue: {arguments.InputQueue}");
            Console.WriteLine($"Output Queue: {arguments.OutputQueue}");

            Console.WriteLine("-------------");

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
                sbc.ReceiveEndpoint(arguments.InputQueue, ep =>
                {
                    ep.ConcurrentMessageLimit = 1;
                    ep.UseMessageRetry(r => r.Immediate(5));
                    ep.Consumer<BidsResequencer>(()=> new BidsResequencer(arguments));
                });
            });
            await bus.StartAsync();
            Console.WriteLine("Bus is running. Waiting for messages...");
            Console.ReadLine();
        }
    }

}