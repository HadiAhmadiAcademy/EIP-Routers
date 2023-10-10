using System.Collections.Immutable;
using Messages.Core;

namespace Router.Resequencers;

public class Resequencer<TCorrelation, TIndex,TMessage> : IResequencer<TCorrelation, TIndex, TMessage> 
                                                        where TMessage : IHaveSequenceId<TIndex>, IHaveCorrelationId<TCorrelation>
                                                        where TIndex : IComparable
{
    private SortedDictionary<TIndex, TMessage> _items = new();
    private readonly TCorrelation _correlationId;
    private IEnumerator<TIndex> _cursor;

    public Resequencer(TCorrelation correlationId, IEnumerator<TIndex> cursor)
    {
        cursor.Reset();
        _correlationId = correlationId;
        this._cursor = cursor;
    }
    public void Add(TMessage message)
    {
        _items.Add(message.SequenceId, message);
    }
    public List<Segment<TCorrelation, TIndex, TMessage>> ExtractCompletedSegments()
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
        return new List<Segment<TCorrelation, TIndex, TMessage>>()
        {
            new(this._correlationId, outputList)
        };
    }
}
