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
                            "1.Send 'PlaceOrder' Command (Iterating Splitter)",
                            "2.Send 'ReservePackage' Command (Composite Message Splitter)",
                            "99.Exit"
                        )).GetIndexOfSelectedChoice();

                if (choice == 1)
                    await SendOrderMessage();
            }
        }

        private static async Task SendOrderMessage()
        {
            long choice = 2;
            PlaceOrder command = null;
            while (choice == 2)
            {
                command = PurchaseOrderFactory.CreateCommand();
                Console.WriteLine(JsonConvert.SerializeObject(command, Formatting.Indented));

                choice = CommandLine.AskAQuestion(a =>
                    a.About("Select the next action:")
                        .WithChoices(
                            "1.Send",
                            "2.Regenerate"
                        )).GetIndexOfSelectedChoice(); 

                if (choice == 1) break;

                Console.Clear();
            }

            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Router"));
            await endpoint.Send<PlaceOrder>(command);
            Console.WriteLine("Sent to Router !");
            Console.WriteLine("------------------------ Press Any Key to Continue ---------------");
            Console.ReadLine();
        }
    }
}