using MassTransit;
using Messages.PackageReservation;
using Newtonsoft.Json;

namespace Consumer1.Handlers;

public class HotelReservationHandler : IConsumer<BookHotel>
{
    public Task Consume(ConsumeContext<BookHotel> context)
    {
        Console.WriteLine("Message Received - BookHotel : ");
        Console.WriteLine("---------------------");
        var json = JsonConvert.SerializeObject(context.Message, Formatting.Indented);
        Console.WriteLine(json);
        Console.WriteLine("---------------------");
        return Task.CompletedTask;
    }
}