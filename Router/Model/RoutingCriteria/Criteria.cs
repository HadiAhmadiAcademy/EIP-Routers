namespace Router.Model.RoutingCriteria;

public abstract class Criteria<T> : ICriteria<T>
{
    public abstract bool SatisfiedBy(T target);

    public Criteria<T> And(ICriteria<T> rightCriteria)
    {
        return new And<T>(this, rightCriteria);
    }
    public Criteria<T> Or(ICriteria<T> rightCriteria)
    {
        return new Or<T>(this, rightCriteria);
    }

    public Criteria<T> Not()
    {
        return new Not<T>(this);
    }
}