using Faker;
using FizzWare.NBuilder;
using Messages.PurchaseOrders;

namespace Client.Factories;

public static class PurchaseOrderFactory
{
    public static PlaceOrder CreateCommand()
    {
        return Builder<PlaceOrder>.CreateNew()
            .With(a => a.VendorId = RandomNumber.Next(1, 10))
            .With(a => a.OrderNumber = RandomNumber.Next(100, 10000))
            .With(a => a.OrderLines = Builder<OrderLine>
                                        .CreateListOfSize(RandomNumber.Next(1, 3))
                                        .Build()
                                        .ToList())
            .Build();
    }
}