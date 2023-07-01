namespace Router.Aggregators;

public interface IHandleAggregation<TInput, TOutput>
{
    public TOutput Handle(TInput input, TOutput currentAggregate);
}