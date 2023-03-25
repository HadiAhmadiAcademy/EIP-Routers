namespace Router.Model.RoutingCriteria;

public class PropertyCriteria<T> : Criteria<T>
{
    private readonly Func<T, bool> _accessor;
    public PropertyCriteria(Func<T, bool> accessor)
    {
        _accessor = accessor;
    }
    public override bool SatisfiedBy(T target)
    {
        return _accessor.Invoke(target);
    }
}