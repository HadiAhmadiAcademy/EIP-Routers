namespace Distributor.Model.RoutingCriteria;

public class And<T> : Criteria<T>
{
    private readonly ICriteria<T> _left;
    private readonly ICriteria<T> _right;
    public And(ICriteria<T> left, ICriteria<T> right)
    {
        _left = left;
        _right = right;
    }

    public override bool SatisfiedBy(T target)
    {
        return _right.SatisfiedBy(target) && _left.SatisfiedBy(target);
    }
}