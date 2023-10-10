using MassTransit;
using Messages.Auctions;

namespace Consumer1.Handlers;

public class BidHandler : IConsumer<BidPlaced>
{
    public Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine($"Bid Received : #CorrelationId:{context.Message.CorrelationId}  - SequenceId:#{context.Message.SequenceId}");
        return Task.CompletedTask;
    }
}