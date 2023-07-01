using System.Collections.Concurrent;
using Messages.Core;
using Router.Core;

namespace Router.Aggregators;

//Testing & Demo purposes only; this implementation has no production use-case
public class InMemoryAggregateRepository<TKey, T> : IAggregateRepository<TKey, T>
{
    private Dictionary<TKey, T> _database = new();

    public Task AddOrUpdate(TKey key, T value)
    {
        if (_database.ContainsKey(key))
            _database[key] = value;
        else
            _database.Add(key, value);
        return Task.CompletedTask;
    }

    public Task<Maybe<T>> Find(TKey key)
    {
        if (_database.ContainsKey(key))
            return Task.FromResult((Maybe<T>)_database[key]);
        return Task.FromResult(Maybe<T>.None);
    }
}