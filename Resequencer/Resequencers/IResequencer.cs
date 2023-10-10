using Messages.Core;

namespace Router.Resequencers;

public interface IResequencer<TCorrelation, TIndex, TMessage> 
                                                where TMessage : IHaveSequenceId<TIndex>, IHaveCorrelationId<TCorrelation>
                                                where TIndex : IComparable
{
    void Add(TMessage message);
    List<Segment<TCorrelation, TIndex, TMessage>> ExtractCompletedSegments();
}