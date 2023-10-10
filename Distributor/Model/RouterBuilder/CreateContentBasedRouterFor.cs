using Distributor.Model.ConflictResolvers;
using Distributor.Model.RoutingCriteria;
using Distributor.Model.Rules;

namespace Distributor.Model.RouterBuilder;

public class ContentBasedRouterBuilder<T> : IRouterConditionBuilder<T>,
                                            IRouterDestinationBuilder<T>
{
    private RoutingTable<T> _routingTable;
    private ICriteria<T> _currentCriteria;
    private IConflictResolvingStrategy<T> _conflictResolver;
    public ContentBasedRouterBuilder()
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
    public IRouterConditionBuilder<T> RouteTo(string channelName)
    {
        var rule = new RoutingRule<T>(_currentCriteria, channelName);
        _routingTable.AddRule(rule);
        return this;
    }
    public IRouterConditionBuilder<T> WhenNoCriteriaMatchesRouteTo(string channelName)
    {
        _routingTable.SetDefaultDestination(channelName);
        return this;
    }
    public IRouterConditionBuilder<T> ResolveConflictsWith(IConflictResolvingStrategy<T> strategy)
    {
        this._conflictResolver = strategy;
        return this;
    }
    public ContentBasedRouter<T> Build()
    {
        return new ContentBasedRouter<T>(_routingTable, _conflictResolver);
    }
}

public interface IRouterConditionBuilder<T>
{
    IRouterConditionBuilder<T> ResolveConflictsWith(IConflictResolvingStrategy<T> strategy);
    IRouterDestinationBuilder<T> When(ICriteria<T> criteria);
    IRouterDestinationBuilder<T> When(Func<T, bool> criteria);
    IRouterConditionBuilder<T> WhenNoCriteriaMatchesRouteTo(string channel);
    ContentBasedRouter<T> Build();
}

public interface IRouterDestinationBuilder<T>
{
    IRouterConditionBuilder<T> RouteTo(string channelName);
}

public static class UseContentBasedRouter
{
    public static IRouterConditionBuilder<T> For<T>()
    {
        return new ContentBasedRouterBuilder<T>();
    }
}