using Messages.Core;
using Router.Core;

namespace Router.Aggregators;

public interface IAggregator<TCorrelationId, TOutput>
{
    Task<Maybe<TOutput>> Aggregate<TInput>(TInput input)
        where TInput : IHaveCorrelationId<TCorrelationId>;
}