using FizzWare.NBuilder;
using Messages.PackageReservation;
using Messages.PurchaseOrders;

namespace Client.Factories;

public class PackageReservationFactory
{
    public static ReservePackage CreateCommand()
    {
        return Builder<ReservePackage>.CreateNew()
            .With(a => a.FlightReservation = Builder<ReserveFlight>.CreateNew().Build())
            .With(a => a.HotelReservation = Builder<BookHotel>.CreateNew().Build())
            .Build();
    }
}