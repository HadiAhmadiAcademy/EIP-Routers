namespace Router.Splitters.Composed;

public class ComposedSplitter<TInput>
{
    private List<Func<TInput, object>> _splitterFunctions;
    public ComposedSplitter(List<Func<TInput, object>> splitterFunctions)
    {
        _splitterFunctions = splitterFunctions;
    }
    public List<object> Split(TInput input)
    {
        return _splitterFunctions
            .Select(a => a.Invoke(input))
            .ToList();
    }
}