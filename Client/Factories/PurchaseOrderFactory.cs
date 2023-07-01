using Faker;
using FizzWare.NBuilder;
using Messages.PurchaseOrders;

namespace Client.Factories;

public static class PurchaseOrderFactory
{
    public static PlaceOrder CreateOrderWithoutLines()
    {
        return Builder<PlaceOrder>.CreateNew()
            .With(a => a.VendorId = RandomNumber.Next(2, 10))
            .With(a => a.OrderNumber = RandomNumber.Next(100, 10000))
            .Build();
    }

    public static List<OrderLine> CreateSomeLines(int count)
    {
        return Builder<OrderLine>.CreateListOfSize(count).Build().ToList();
    }
}