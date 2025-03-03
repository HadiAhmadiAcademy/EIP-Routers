using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Client.CommandLineInterface;
using Client.Factories;
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
                            "1. Send 100 Random Messages",
                            "99.Exit"
                        )).GetIndexOfSelectedChoice();

                if (choice == 1)
                    await SendOrderMessages();
            }
        }

        private static async Task SendOrderMessages()
        {
            for (int i = 1; i <= 100; i++)
            {
                var command = PurchaseOrderFactory.CreateCommand();
                command.OrderNumber = i;

                var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Consumer"));
                await endpoint.Send(command);

                Console.WriteLine($"#{i} Sent");
            }

            Console.WriteLine("------------------------ Completed !");
            Console.WriteLine("------------------------ Press Any Key to Continue ---------------");
            Console.ReadLine();
        }
    }
}