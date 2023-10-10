using System.Collections.Concurrent;
using Messages.Core;

namespace Router.Resequencers;

public class MultiResequencer<TCorrelation, TIndex, TMessage> : IResequencer<TCorrelation, TIndex, TMessage>
                    where TMessage : IHaveSequenceId<TIndex>, IHaveCorrelationId<TCorrelation>
                    where TIndex : IComparable
{
    private ConcurrentDictionary<TCorrelation, IResequencer<TCorrelation, TIndex, TMessage>> _resequencers = new();
    private Func<TMessage, IResequencer<TCorrelation, TIndex, TMessage>> _resequencerFactory;
    public MultiResequencer(Func<TMessage, IResequencer<TCorrelation, TIndex, TMessage>> resequencerFactory)
    {
        _resequencerFactory = resequencerFactory;
    }

    public void Add(TMessage message)
    {
        var resequencerInstance = _resequencers.GetOrAdd(message.CorrelationId, a => _resequencerFactory.Invoke(message));
        resequencerInstance.Add(message);
    }

    public List<Segment<TCorrelation, TIndex, TMessage>> ExtractCompletedSegments()
    {
        return _resequencers.SelectMany(a => a.Value.ExtractCompletedSegments())
            .Where(a=> a.Data.Any())
            .ToList();
    }
}