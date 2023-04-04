using Router.Model.RoutingCriteria;
using Router.Model.Rules;

namespace Router.Model.RouterBuilder;

public class RecipientListBuilder<T> : IRouterConditionBuilder<T>,
                                        IRouterDestinationBuilder<T>
{
    private RoutingTable<T> _routingTable;
    private ICriteria<T> _currentCriteria;
    public RecipientListBuilder()
    {
        this._routingTable = new RoutingTable<T>();
    }
    public IRouterDestinationBuilder<T> When(ICriteria<T> criteria)
    {
        _currentCriteria = criteria;
        return this;
    }
    public IRouterDestinationBuilder<T> When(Func<T, bool> criteria)
    {
        _currentCriteria = new PropertyCriteria<T>(criteria);
        return this;
    }
    public IRouterConditionBuilder<T> RouteTo(params string[] channelNames)
    {
        return RouteTo(channelNames.ToList());
    }
    public IRouterConditionBuilder<T> RouteTo(List<string> channelNames)
    {
        var rule = new RoutingRule<T>(_currentCriteria, channelNames);
        _routingTable.AddRule(rule);
        return this;
    }
    public IRouterConditionBuilder<T> WhenNoCriteriaMatchesRouteTo(params string[] defaultChannelNames)
    {
        return WhenNoCriteriaMatchesRouteTo(defaultChannelNames.ToList());
    }
    public IRouterConditionBuilder<T> WhenNoCriteriaMatchesRouteTo(List<string> defaultChannelNames)
    {
        _routingTable.SetDefaultDestinations(defaultChannelNames);
        return this;
    }
    
    public RecipientList<T> Build()
    {
        return new RecipientList<T>(_routingTable);
    }
}

public interface IRouterConditionBuilder<T>
{
    IRouterDestinationBuilder<T> When(ICriteria<T> criteria);
    IRouterDestinationBuilder<T> When(Func<T, bool> criteria);
    IRouterConditionBuilder<T> WhenNoCriteriaMatchesRouteTo(params string[] channels);
    IRouterConditionBuilder<T> WhenNoCriteriaMatchesRouteTo(List<string> channels);
    RecipientList<T> Build();
}

public interface IRouterDestinationBuilder<T>
{
    IRouterConditionBuilder<T> RouteTo(params string[] channelNames);
    IRouterConditionBuilder<T> RouteTo(List<string> channelNames);
}

public static class UseRecipientList
{
    public static IRouterConditionBuilder<T> For<T>()
    {
        return new RecipientListBuilder<T>();
    }
}