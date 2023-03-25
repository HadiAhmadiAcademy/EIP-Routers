using DequeNet;
using MassTransit;
using Messages.PurchaseOrders;
using Router.Handlers;
using Router.Model.RouterBuilder;

namespace Router
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var router = UseContentBasedRouter.For<PlaceOrder>()
                    .When(a => a.VendorId == 1).RouteTo("queue:Consumer1")
                    .When(a => a.VendorId == 2).RouteTo("queue:Consumer2")
                    .WhenNoCriteriaMatchesRouteTo("queue:Consumer3")
                    .Build();

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
                sbc.ReceiveEndpoint("Router", ep =>
                {
                    ep.UseMessageRetry(r => r.Immediate(5));
                    ep.Consumer(typeof(PlaceOrderHandler), a => new PlaceOrderHandler(router));
                });
            });
            await bus.StartAsync();
            Console.WriteLine("Bus is running. Waiting for messages...");
            Console.ReadLine();
        }
    }
}