using CommandLine;
using Consumer1.Handlers;
using MassTransit;
using Router.Core;

namespace Consumer1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var arguments = Parser.Default.ParseArguments<ConsumerArguments>(args).Value;

            Console.WriteLine($"----- {arguments.InputQueue} ----- ");

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
                sbc.ReceiveEndpoint(arguments.InputQueue, ep =>
                {
                    ep.Consumer<BidHandler>();
                });
            });
            await bus.StartAsync();

            Console.WriteLine("Bus Started. Waiting for Messages...");
            Console.WriteLine("------------------------------------");
            Console.ReadLine();
        }
    }
}