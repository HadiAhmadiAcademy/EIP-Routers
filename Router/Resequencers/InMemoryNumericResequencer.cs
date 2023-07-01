using Messages.Core;

namespace Router.Resequencers;

public class InMemoryNumericResequencer<T> : IResequencer<long, T> where T : IHaveSequenceId<long>
{
    private SortedDictionary<long, T> _items = new();
    private long currentSequence = 1;
    public void Add(T entity)
    {
        _items.Add(entity.SequenceId, entity);
    }
    public SortedList<long, T> ExtractCompletedSegment()
    {
        var outputList = new SortedList<long, T>();
        while (true)
        {
            if (_items.ContainsKey(currentSequence))
            {
                outputList.Add(currentSequence, _items[currentSequence]);
                _items.Remove(currentSequence);
            }
            else
                break;

            currentSequence++;
        }
        return outputList;
    }
}