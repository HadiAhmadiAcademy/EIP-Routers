using MassTransit;
using Messages.Auctions;

namespace Consumer1.Handlers;

public class BidHandler : IConsumer<BidPlaced>
{
    public Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine($"Bid Received : #{context.Message.SequenceId} - Amount : {context.Message.BidAmount}");

        return Task.CompletedTask;
    }
}