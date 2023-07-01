using System.Security.AccessControl;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Client.CommandLineInterface;
using Client.Factories;
using Faker;
using MassTransit;
using Messages.Core;
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
            long cursor = 1;

            while (true)
            {
                Console.Clear();
                var choice = CommandLine.AskAQuestion(a=> 
                    a.About("Select the next action:")
                        .WithChoices(
                            "1.Send a sequence of  5 'BidPlaced' randomly disordered",
                            "99.Exit"
                        )).GetIndexOfSelectedChoice();


                if (choice == 1)
                {
                    var bids = BidFactory.CreateSomeBidsWithRandomOrder(cursor, cursor + 5);
                    cursor += 5;

                    Console.WriteLine("-- Bid Placed Events:");
                    var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Router"));

                    foreach (var bidPlaced in bids)
                    {
                        Console.WriteLine($"#{bidPlaced.SequenceId} - Amount : {bidPlaced.BidAmount}");
                        await endpoint.Send(bidPlaced);
                        Thread.Sleep(3000);
                    }

                    Console.WriteLine("------------------------ Sent !------ ---------------");


                    
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