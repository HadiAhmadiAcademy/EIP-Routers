namespace Router.Model.ConflictResolvers;

public class PickFirst<T> : IConflictResolvingStrategy<T>
{
    public static IConflictResolvingStrategy<T> Instance = new PickFirst<T>();
    private PickFirst() { }
    public string Resolve(T message, List<string> possibleDestinations)
    {
        return possibleDestinations.First();
    }
}