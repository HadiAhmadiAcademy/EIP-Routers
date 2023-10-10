using System.Collections;

namespace Router.Resequencers;

public class InfiniteNumericEnumerator : IEnumerator<long>
{
    private readonly long _startingIndex = 0;
    private long _currentIndex = 0;
    public InfiniteNumericEnumerator(long startingIndex)
    {
        this._startingIndex = startingIndex;
        this._currentIndex = startingIndex;
    }
    public InfiniteNumericEnumerator() { }
    public bool MoveNext()
    {
        _currentIndex++;
        return true;
    }
    public void Reset()
    {
        this._currentIndex = _startingIndex;
    }
    public long Current => _currentIndex;
    object IEnumerator.Current => Current;
    public void Dispose()
    {
    }
}