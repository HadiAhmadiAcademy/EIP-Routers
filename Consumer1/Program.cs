using Consumer1.Handlers;
using MassTransit;

namespace Consumer1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Consumer 1";
            Console.WriteLine("----- Consumer 1 ----- ");

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
                sbc.ReceiveEndpoint("Consumer", ep =>
                {
                    ep.Consumer<PlaceOrderHandler>();
                });
            });
            await bus.StartAsync();

            Console.WriteLine("Bus Started. Waiting for Messages...");
            Console.WriteLine("------------------------------------");
            Console.ReadLine();
        }
    }
}