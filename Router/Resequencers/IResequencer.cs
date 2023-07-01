using Messages.Core;

namespace Router.Resequencers;

public interface IResequencer<TIndex, TEntity> where TEntity : IHaveSequenceId<TIndex>
{
    void Add(TEntity entity);
    SortedList<TIndex, TEntity> ExtractCompletedSegment();
}