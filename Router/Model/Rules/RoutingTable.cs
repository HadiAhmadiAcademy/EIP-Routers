using System.Threading.Channels;
using Router.Model.Constants;

namespace Router.Model.Rules;

public class RoutingTable<T>
{
    private List<RoutingRule<T>> _rules;
    private string _defaultDestination;
    public RoutingTable()
    {
        _rules = new List<RoutingRule<T>>();
        _defaultDestination = Channels.NullChannel;
    }
    public void AddRule(RoutingRule<T> rule)
    {
        _rules.Add(rule);
    }
    public void SetDefaultDestination(string channelName)
    {
        this._defaultDestination = channelName;
    }

    public List<string> FindDestinationsForMessage(T message)
    {
        var destinations = _rules.Where(a => a.IsSatisfiedByCriteria(message))
            .Select(a => a.Destination)
            .ToList();

        if (!destinations.Any()) 
            destinations.Add(_defaultDestination);

        return destinations;
    }
}