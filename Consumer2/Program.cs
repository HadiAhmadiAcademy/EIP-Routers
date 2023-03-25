using Consumer2.Handlers;
using MassTransit;

namespace Consumer2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("----- Consumer 2 ----- ");

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
                sbc.ReceiveEndpoint("Consumer2", ep =>
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