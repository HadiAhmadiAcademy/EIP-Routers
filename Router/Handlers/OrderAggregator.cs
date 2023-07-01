using Messages.Core;
using Messages.PurchaseOrders;
using Router.Aggregators;
using Router.Core;

namespace Router.Handlers;

public class OrderAggregator : Aggregator<long, CompletedOrder>,
                                IHandleAggregation<PlaceOrder, CompletedOrder>
{
    public OrderAggregator(IAggregateRepository<long, CompletedOrder> aggregateRepository) 
        : base(aggregateRepository)
    {
    }
    public OrderAggregator(): base(new InMemoryAggregateRepository<long, CompletedOrder>())
    {
        
    }

    protected override bool IsCompleted(CompletedOrder aggregateInstance)
    {
        return aggregateInstance.ProcessInfo != null && 
               aggregateInstance.ProcessInfo.IsAggregationCompleted();
    }

    public CompletedOrder Handle(PlaceOrder input, CompletedOrder currentAggregate)
    {
        currentAggregate.ProcessInfo ??= new ProcessInfo(input.SliceInfo.Total);
        currentAggregate.ProcessInfo.IncreaseProcessedCount();

        currentAggregate.OrderNumber = input.OrderNumber;
        currentAggregate.VendorId = input.VendorId;
        currentAggregate.OrderLines.AddRange(input.OrderLines);

        return currentAggregate;
    }
}