using System.Collections.Concurrent;

namespace Consumer2.Services;

public interface IInbox
{
    Task<bool> IsAlreadyProcessed(Guid messageId);
    Task AddToProcessed(Guid messageId);
}

public class InMemoryInbox : IInbox     //Just a for using in Demo, not a valid implementation
{
    private static ConcurrentDictionary<Guid, DateTime> _table = new ConcurrentDictionary<Guid, DateTime>();

    public Task<bool> IsAlreadyProcessed(Guid messageId)
    {
        return Task.FromResult(_table.ContainsKey(messageId));
    }

    public Task AddToProcessed(Guid messageId)
    {
        _table.TryAdd(messageId, DateTime.Now);
        return Task.CompletedTask;
    }
}