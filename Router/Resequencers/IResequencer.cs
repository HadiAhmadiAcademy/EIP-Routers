using Messages.Core;

namespace Router.Resequencers;

public interface IResequencer<TIndex, TMessage> where TMessage : IHaveSequenceId<TIndex>
                                                where TIndex : IComparable
{
    void Add(TMessage message);
    SortedList<TIndex, TMessage> ExtractCompletedSegment();
}