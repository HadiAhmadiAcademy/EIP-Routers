using Messages.Core;
using Router.Core;

namespace Router.Aggregators;

public interface IAggregateRepository<TKey, T>
{
    public Task AddOrUpdate(TKey key, T value);

    public Task<Maybe<T>> Find(TKey key);
}