using Faker;
using FizzWare.NBuilder;
using Messages.Auctions;

namespace Client.Factories;

public static class BidFactory
{
    public static List<BidPlaced> CreateSomeBidsWithRandomOrder(long startingSequence, long endingSequence)
    {
        var listOfBids = new List<BidPlaced>();
        for (var i = startingSequence; i < endingSequence; i++)
        {
            listOfBids.Add(new BidPlaced()
            {
                BidAmount = Faker.RandomNumber.Next(1000, 10000),
                SequenceId = i,
                EventId = Guid.NewGuid(),
                MessageId = Guid.NewGuid(),
                PublishedDateTime = DateTime.Now
            });
        }
        listOfBids = listOfBids.Shuffle();
        return listOfBids;
    }
}