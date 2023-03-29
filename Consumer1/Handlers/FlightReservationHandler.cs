using MassTransit;
using Messages.PackageReservation;
using Newtonsoft.Json;

namespace Consumer1.Handlers;

public class FlightReservationHandler : IConsumer<ReserveFlight>
{
    public Task Consume(ConsumeContext<ReserveFlight> context)
    {
        Console.WriteLine("Message Received - ReserveFlight : ");
        Console.WriteLine("---------------------");
        var json = JsonConvert.SerializeObject(context.Message, Formatting.Indented);
        Console.WriteLine(json);
        Console.WriteLine("---------------------");
        return Task.CompletedTask;
    }
}