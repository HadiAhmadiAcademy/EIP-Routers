namespace Distributor.Model.RoutingCriteria;

public class Not<T> : Criteria<T>
{
    private readonly ICriteria<T> _targetCriteria;
    public Not(ICriteria<T> targetCriteria)
    {
        this._targetCriteria = targetCriteria;
    }

    public override bool SatisfiedBy(T target)
    {
        return !_targetCriteria.SatisfiedBy(target);
    }
}