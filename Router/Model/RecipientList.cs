using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Router.Model.Rules;

namespace Router.Model;

public class RecipientList<T> : IRecipientList<T>
{
    private readonly RoutingTable<T> _routingTable;
    public RecipientList(RoutingTable<T> routingTable)
    {
        this._routingTable = routingTable;
    }

    public List<string> FindDestinationsFor(T message)
    {
        var destinations = _routingTable.FindDestinationsForMessage(message);
        return destinations;
    }
}

