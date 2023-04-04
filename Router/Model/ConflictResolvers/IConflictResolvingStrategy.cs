namespace Router.Model.ConflictResolvers;

public interface IConflictResolvingStrategy<T>
{
    public string Resolve(T message, List<string> possibleDestinations);
}
