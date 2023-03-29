using MassTransit;
using Messages.PackageReservation;
using Router.Splitters.Composed;

namespace Router.Handlers;

public class ReservePackageHandler : IConsumer<ReservePackage>
{
    private static ComposedSplitter<ReservePackage> _splitter;
    static ReservePackageHandler()
    {
        _splitter = CreateComposedSplitter.WhichTakesInput<ReservePackage>()
            .SplitBy(a => a.FlightReservation)
            .SplitBy(a=> a.HotelReservation)
            .Build();
    }


    public async Task Consume(ConsumeContext<ReservePackage> context)
    {
        var individualCommands = _splitter.Split(context.Message);

        foreach (var command in individualCommands)
            await context.Send(new Uri("queue:Consumer1"), command);
    }
}