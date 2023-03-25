using Router.Model.RoutingCriteria;

namespace Router.Model.Rules;

public class RoutingRule<T>
{
    public ICriteria<T> Criteria { get; set; }
    public string Destination { get; set; }
    public RoutingRule(ICriteria<T> criteria, string destination)
    {
        Criteria = criteria;
        this.Destination = destination;
    }
    public bool IsSatisfiedByCriteria(T message)
    {
        return Criteria.SatisfiedBy(message);
    }
}