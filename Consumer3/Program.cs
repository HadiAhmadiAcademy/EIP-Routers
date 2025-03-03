using Consumer3.Handlers;
using MassTransit;

namespace Consumer3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Consumer 3";
            Console.WriteLine("----- Consumer 3 ----- ");

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
                sbc.ReceiveEndpoint("Consumer", ep =>
                {
                    ep.Consumer<PlaceOrderHandler>();
                    ep.ConcurrentMessageLimit = 1;
                });
            });
            await bus.StartAsync();

            Console.WriteLine("Bus Started. Waiting for Messages...");
            Console.WriteLine("------------------------------------");
            Console.ReadLine();
        }
    }
}