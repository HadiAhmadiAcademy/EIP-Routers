using Router.Model.RoutingCriteria;

namespace Router.Model.Rules;

public class RoutingRule<T>
{
    public ICriteria<T> Criteria { get; set; }
    public List<string> Destinations { get; set; }
    public RoutingRule(ICriteria<T> criteria, List<string> destinations)
    {
        Criteria = criteria;
        this.Destinations = destinations;
    }
    public bool IsSatisfiedByCriteria(T message)
    {
        return Criteria.SatisfiedBy(message);
    }
}