using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using MassTransit;
using Messages.Auctions;
using Newtonsoft.Json;
using Router.Core;
using Router.Resequencers;

namespace Router.Handlers;

public class BidsResequencer : IConsumer<BidPlaced>
{
    private readonly RouterArguments _arguments;
    private static MultiResequencer<long, long, BidPlaced> _resequencer = CreateResequencer();
    public BidsResequencer(RouterArguments arguments)
    {
        _arguments = arguments;
    }

    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine($"Message Received - CorrelationId:{context.Message.CorrelationId} | SequenceId:{context.Message.SequenceId}");
        _resequencer.Add(context.Message);

        var segments = _resequencer.ExtractCompletedSegments();

        if (!segments.Any()) return;

        foreach (var segment in segments)
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Segment #{segment.CorrelationId} : ");
            Console.WriteLine($"Sequence Batch Completed - Correlation:{segment.CorrelationId} - " + $": ({segment.Data.First().Key} - {segment.Data.Last().Key})");
            var endpoint = await context.GetSendEndpoint(new Uri($"queue:{_arguments.OutputQueue}"));
            foreach (var bidPlaced in segment.Data)
            {
                await endpoint.Send(bidPlaced.Value);
                Console.WriteLine($"#{bidPlaced.Value} Sent to '{_arguments.OutputQueue}'...");
            }
            Console.WriteLine("-----------------------------------------");
        }
    }
    private static MultiResequencer<long, long, BidPlaced> CreateResequencer()
    {
        return new MultiResequencer<long, long, BidPlaced>(a =>
            new Resequencer<long, long, BidPlaced>(a.CorrelationId, new InfiniteNumericEnumerator(1)));
    }
}