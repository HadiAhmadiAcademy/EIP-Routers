using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Client.CommandLineInterface;
using Client.Factories;
using Faker;
using MassTransit;
using Messages.Core;
using Messages.Core.DataPartitioning;
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
                            "1.Send Parts of a splitted 'PlaceOrder' ",
                            "99.Exit"
                        )).GetIndexOfSelectedChoice();

                if (choice == 1)
                {
                    var order = PurchaseOrderFactory.CreateOrderWithoutLines();
                    var countOfLines = RandomNumber.Next(2, 6);
                    var orderLines = PurchaseOrderFactory.CreateSomeLines(countOfLines);

                    Console.WriteLine("-- Order Body :");
                    Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                    Console.WriteLine("-- Order Lines :");
                    Console.WriteLine(JsonConvert.SerializeObject(orderLines, Formatting.Indented));
                    Console.WriteLine($"Press to send ({countOfLines}) messages");
                    Console.ReadLine();

                    var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Router"));

                    for (int i = 0; i < countOfLines; i++)
                    {
                        order.OrderLines = new List<OrderLine>() { orderLines[i] };
                        order.MessageId = Guid.NewGuid();
                        order.SliceInfo = new SliceInfo(i +1, countOfLines);
                        await endpoint.Send(order);
                        Console.WriteLine($"Message #{i+1} Sent to Router !");
                        Console.WriteLine("Wait 2 seconds...");
                        Thread.Sleep(2000);
                    }
                    Console.WriteLine("------------------------ Press Any Key to Continue ---------------");
                    Console.ReadLine();
                }

            }
        }

        private static async Task SendCommand<T>(Func<T> factory) where T : class, ICommand
        {
            long choice = 2;
            T command = null;
            while (choice == 2)
            {
                command = factory();
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
            await endpoint.Send<T>(command);
            Console.WriteLine("Sent to Router !");
            Console.WriteLine("------------------------ Press Any Key to Continue ---------------");
            Console.ReadLine();
        }
    }
}