namespace Router.Splitters.Iterating;

public static class CreateIteratingSplitter
{
    public static IteratingSplitterInputBuilder Which()
    {
        return new IteratingSplitterInputBuilder();
    }
}

public class IteratingSplitterInputBuilder
{
    public IteratingSplitterOutputBuilder<TInput> TakesInput<TInput>()
    {
        return new IteratingSplitterOutputBuilder<TInput>();
    }
}
public class IteratingSplitterOutputBuilder<TInput>
{
    public IteratingSplitterBuilder<TInput, TOutput> Produces<TOutput>()
    {
        return new IteratingSplitterBuilder<TInput, TOutput>();
    }
}

public class IteratingSplitterBuilder<TInput, TOutput>
{
    private Func<TInput, int> _iteration;
    private Func<TInput, int, TOutput> _converter;
    public IteratingSplitterBuilder<TInput, TOutput> SplitBasedOn(Func<TInput, int> iteration)
    {
        this._iteration = iteration;
        return this;
    }
    public IteratingSplitterBuilder<TInput, TOutput> UsingConverter(Func<TInput, int, TOutput> converter)
    {
        this._converter = converter;
        return this;
    }
    public IteratingSplitter<TInput, TOutput> Build()
    {
        return new IteratingSplitter<TInput, TOutput>(this._iteration, this._converter);
    }
}