using Messages.Core;

namespace Messages.PackageReservation;

public class BookHotel : ICommand
{
    public long HotelId { get; set; }
    public RoomType RoomType { get; set; }
    public int Count { get; set; }
    public Guid MessageId { get; } = Guid.NewGuid();
}

public enum RoomType
{
    Twin,
    Deluxe,
    DoubleDeluxe,
    Suit,
}