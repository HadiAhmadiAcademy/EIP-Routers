namespace Messages.Core.DataPartitioning;

public class SliceInfo
{
    public long Current { get; private set; }
    public long Total { get; private set; }
    public SliceInfo(long current, long total)
    {
        Current = current;
        Total = total;
    }
}