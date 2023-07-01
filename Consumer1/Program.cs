using Consumer1.Handlers;
using MassTransit;

namespace Consumer1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("----- Consumer 1 ----- ");

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
                sbc.ReceiveEndpoint("Consumer1", ep =>
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