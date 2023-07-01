using Messages.Core;
using Router.Core;

namespace Router.Aggregators;

public abstract class Aggregator<TCorrelationId, TOutput> : IAggregator<TCorrelationId, TOutput>
    where TOutput : new()
{
    protected IAggregateRepository<TCorrelationId, TOutput> AggregateRepository;
    protected Aggregator(IAggregateRepository<TCorrelationId, TOutput> aggregateRepository)
    {
        AggregateRepository = aggregateRepository;
    }

    public virtual async Task<Maybe<TOutput>> Aggregate<TInput>(TInput input)
        where TInput : IHaveCorrelationId<TCorrelationId>
    {
        var handler = FindHandler<TInput>();

        var aggregateInstance = await AggregateRepository.Find(input.CorrelationId);
        if (aggregateInstance.HasNoValue)
            aggregateInstance = new TOutput();

        aggregateInstance = handler.Handle(input, aggregateInstance);

        await AggregateRepository.AddOrUpdate(input.CorrelationId, aggregateInstance);

        if (IsCompleted(aggregateInstance))
            return aggregateInstance;
        else
            return Maybe<TOutput>.None;
    }

    private IHandleAggregation<TInput, TOutput> FindHandler<TInput>() 
    {
        var handler = this as IHandleAggregation<TInput, TOutput>;
        if (handler == null)
            throw new HandlerNotFoundException(this.GetType().Name, typeof(TInput).Name);
        return handler;
    }

    protected abstract bool IsCompleted(TOutput aggregateInstance);
}