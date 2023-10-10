using System.Security.AccessControl;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Client.CommandLineInterface;
using Client.Factories;
using Faker;
using MassTransit;
using Messages.Auctions;
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
                            "1.Send 5 out-of-order messages with 5 different correlation ids",
                            "99.Exit"
                        )).GetIndexOfSelectedChoice();

                if (choice == 1)
                {
                    var setOfBids = new Dictionary<long, List<BidPlaced>>();

                    for (int i = 1; i <= 5; i++)
                    {
                        var bids = BidFactory.CreateSomeBidsWithRandomOrder(cursor, cursor + 5, i);
                        setOfBids.Add(i, bids);
                    }
                    cursor += 5;

                    Console.WriteLine("Preview :");
                    foreach (var set in setOfBids)
                    {
                        Console.WriteLine($"-------------Correlation Id : {set.Key}------------------");
                        Console.WriteLine(string.Join(" | ", set.Value.Select(a=> a.SequenceId)));
                        Console.WriteLine($"---------------------------------------------------------");
                    }

                    Console.WriteLine("Press any key to mix sets and send them...");
                    Console.ReadLine();


                    Console.WriteLine("-- Bid Placed Events:");
                    var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Router"));

                    var mixedList = setOfBids.SelectMany(a => a.Value).ToList().Shuffle();

                    foreach (var bidPlaced in mixedList)
                    {
                        Console.WriteLine($"Correlation:#{bidPlaced.OrderId}  -  Sequence:#{bidPlaced.SequenceId}");
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