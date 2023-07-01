namespace Messages.PurchaseOrders;

public class ProcessInfo
{
    public long ProcessedCount { get; private set; }
    public long? TotalCount { get; private set; }
    public ProcessInfo(long? totalCount)
    {
        TotalCount = totalCount;
    }
    public void IncreaseProcessedCount()
    {
        this.ProcessedCount++;
    }

    public bool IsAggregationCompleted()
    {
        if (TotalCount == null) return false;
        return this.ProcessedCount >= TotalCount.Value;
    }
}