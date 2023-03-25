namespace Router.Model.RoutingCriteria;

public interface ICriteria<in T>
{
    bool SatisfiedBy(T item);
}