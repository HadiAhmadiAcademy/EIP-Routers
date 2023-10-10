namespace Router.Resequencers;

public class Segment<TCorrelation, TIndex, TMessage>
{
    public TCorrelation CorrelationId { get; private set; }
    public SortedList<TIndex, TMessage> Data { get; private set; }
    public Segment(TCorrelation correlationId, SortedList<TIndex, TMessage> data)
    {
        Data = data;
        CorrelationId = correlationId;
    }
}