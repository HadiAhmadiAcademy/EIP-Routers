using Messages.Core;

namespace Router.Resequencers;

public class Resequencer<TIndex,TMessage> : IResequencer<TIndex, TMessage> where TMessage : IHaveSequenceId<TIndex>
                                                                            where TIndex : IComparable
{
    private SortedDictionary<TIndex, TMessage> _items = new();
    private IEnumerator<TIndex> _cursor;

    public Resequencer(IEnumerator<TIndex> cursor)
    {
        cursor.Reset();
        this._cursor = cursor;
    }
    public void Add(TMessage message)
    {
        _items.Add(message.SequenceId, message);
    }
    public SortedList<TIndex, TMessage> ExtractCompletedSegment()
    {
        var outputList = new SortedList<TIndex, TMessage>();
        while (true)
        {
            if (_items.ContainsKey(_cursor.Current))
            {
                outputList.Add(_cursor.Current, _items[_cursor.Current]);
                _items.Remove(_cursor.Current);
            }
            else
                break;

            _cursor.MoveNext();
        }
        return outputList;
    }
}
