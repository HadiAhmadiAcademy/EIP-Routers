using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Client.CommandLineInterface;
using Faker;
using MassTransit;
using Messages.PurchaseOrders;
using Newtonsoft.Json;
using Spectre.Console;

namespace Client
{
    internal class Program
    {
        private static IBusControl _bus;
        static async Task Main(string[] args)
        {
            Console.Title = "Client";

            _bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
            });
            await _bus.StartAsync();

            while (true)
            {
                Console.Clear();
                var choice = CommandLine.AskAQuestion(a=> 
                    a.About("Select the next action:")
                        .WithChoices(
                            "1.Send 4 Duplicate 'RemoveOrder' Command - (Semantic Sample)",
                            "2.Send 4 Duplicate 'SendPlaceOrder' Command - (Inbox Sample)",
                            "99.Exit"
                        )).GetIndexOfSelectedChoice();

                if (choice == 1)
                    await SendRemoveOrder();
                if (choice == 2)
                    await SendPlaceOrder();
            }
        }

        private static async Task SendRemoveOrder()
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Consumer1"));

            var orderId = RandomNumber.Next(1, 1000);
            Console.WriteLine($"Start Sending Duplicate Messages for Order {orderId}...");

            for (int i = 0; i < 4; i++)
            {
                var command = new RemoveOrder()
                {
                    OrderId = orderId,
                    MessageId = Guid.NewGuid()
                };

                await endpoint.Send<RemoveOrder>(command);
                Console.WriteLine($"#{i} Sent !");
                Thread.Sleep(1000);
            }
            Console.WriteLine("------------------------ Press Any Key to Continue ---------------");
            Console.ReadLine();
        }

        private static async Task SendPlaceOrder()
        {
            var command = new PlaceOrder
            {
                MessageId = Guid.NewGuid(),
            };

            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Consumer2"));

            Console.WriteLine("Start Sending Duplicate Messages...");

            for (int i = 0; i < 4; i++)
            {
                await endpoint.Send<PlaceOrder>(command);
                Console.WriteLine($"#{i} Sent !");
                Thread.Sleep(1000);
            }
            Console.WriteLine("------------------------ Press Any Key to Continue ---------------");
            Console.ReadLine();
        }

    }
}