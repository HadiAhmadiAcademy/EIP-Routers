using System.Collections;
using Distributor.Model.ConflictResolvers;
using Distributor.Model.Rules;

namespace Distributor.Model;

public class ContentBasedRouter<T> : IContentBasedRouter<T>
{
    private readonly RoutingTable<T> _routingTable;
    private readonly IConflictResolvingStrategy<T> _conflictResolver;
    public ContentBasedRouter(RoutingTable<T> routingTable, IConflictResolvingStrategy<T>? conflictResolver)
    {
        this._routingTable = routingTable;
        this._conflictResolver = conflictResolver ?? PickFirst<T>.Instance;
    }
    public string FindDestinationFor(T message)
    {
        var destinations = _routingTable.FindDestinationsForMessage(message);
        if (IsConflictDetected(destinations))
            return _conflictResolver.Resolve(message, destinations);

        return destinations.First();
    }
    private static bool IsConflictDetected(ICollection destinations)
    {
        return destinations.Count > 1;
    }
}

