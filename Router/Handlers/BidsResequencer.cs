using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using MassTransit;
using Messages.Auctions;
using Newtonsoft.Json;
using Router.Resequencers;

namespace Router.Handlers;

public class BidsResequencer : IConsumer<BidPlaced>
{
    private static IResequencer<long, BidPlaced> Resequencer = new InMemoryNumericResequencer<BidPlaced>();
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine($"Message #{context.Message.SequenceId} Received...");
        Resequencer.Add(context.Message);
        var sequence = Resequencer.ExtractCompletedSegment();

        if (!sequence.Any()) return;


        Console.WriteLine($"Sequence Batch Completed " + $": ({sequence.First().Key} - {sequence.Last().Key})");
        var endpoint = await context.GetSendEndpoint(new Uri("queue:Consumer1"));
        foreach (var bidPlaced in sequence)
        {
            await endpoint.Send(bidPlaced.Value);
            Console.WriteLine($"#{bidPlaced.Value} Sent...");
        }

        Console.WriteLine("-----------------------------------------");
    }
}