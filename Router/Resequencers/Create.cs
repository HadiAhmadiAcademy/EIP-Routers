using Messages.Core;

namespace Router.Resequencers;

public static class Create
{
    public static IResequencer<long, TMessage> NumericResequencerFor<TMessage>(long startFrom = 1) 
        where TMessage : IHaveSequenceId<long>
    {
        return new Resequencer<long, TMessage>(new InfiniteNumericEnumerator(startFrom));
    }
}