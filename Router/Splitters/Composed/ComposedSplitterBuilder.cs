namespace Router.Splitters.Composed;

public static class CreateComposedSplitter
{
    public static ComposedSplitterBuilder<TInput> WhichTakesInput<TInput>()
    {
        return new ComposedSplitterBuilder<TInput>();
    }
}

public class ComposedSplitterBuilder<TInput>
{
    private readonly List<Func<TInput, object>> _splitters = new List<Func<TInput, object>>();
    public ComposedSplitterBuilder<TInput> SplitBy(Func<TInput, object> selector)
    {
        this._splitters.Add(selector);
        return this;
    }
    public ComposedSplitter<TInput> Build()
    {
        return new ComposedSplitter<TInput>(_splitters);
    }
}