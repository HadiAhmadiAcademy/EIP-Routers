using Mapster;

namespace Router.Splitters.Iterating;

public class IteratingSplitter<TInput, TOutput>
{
    private readonly Func<TInput, int> _iteration;
    private readonly Func<TInput, int, TOutput> _splitterFunc;
    public IteratingSplitter(Func<TInput, int> iteration, Func<TInput, int, TOutput> splitterFunc)
    {
        _iteration = iteration;
        _splitterFunc = splitterFunc;
    }
    public List<TOutput> Split(TInput input)
    {
        var outputList = new List<TOutput>();
        var loopCount = _iteration.Invoke(input);
        for (int i = 0; i < loopCount; i++)
        {
            var item = _splitterFunc.Invoke(input, i);
            outputList.Add(item);
        }
        return outputList;
    }
}

