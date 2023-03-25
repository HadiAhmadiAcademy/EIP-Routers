namespace Router.Model.RoutingCriteria;

public class Or<T> : Criteria<T>
{
    private readonly ICriteria<T> _left;
    private readonly ICriteria<T> _right;
    public Or(ICriteria<T> left, ICriteria<T> right)
    {
        _left = left;
        _right = right;
    }
    public override bool SatisfiedBy(T target)
    {
        return _right.SatisfiedBy(target) || _left.SatisfiedBy(target);
    }
}