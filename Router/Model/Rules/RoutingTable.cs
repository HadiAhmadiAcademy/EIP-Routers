using System.Threading.Channels;
using MoreLinq;
using Router.Model.Constants;

namespace Router.Model.Rules;

public class RoutingTable<T>
{
    private List<RoutingRule<T>> _rules;
    private List<string> _defaultDestinations;
    public RoutingTable()
    {
        _rules = new List<RoutingRule<T>>();
        _defaultDestinations = new List<string>() { Channels.NullChannel };
    }
    public void AddRule(RoutingRule<T> rule)
    {
        _rules.Add(rule);
    }
    public void SetDefaultDestinations(List<string> channelNames)
    {
        this._defaultDestinations = channelNames;
    }

    public List<string> FindDestinationsForMessage(T message)
    {
        return _rules.Where(a => a.IsSatisfiedByCriteria(message))
            .SelectMany(a => a.Destinations)
            .FallbackIfEmpty(_defaultDestinations)
            .ToList();
    }
}