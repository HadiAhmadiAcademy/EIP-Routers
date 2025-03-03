using Consumer2.Services;
using MassTransit;
using Messages.Core;
using Messages.PurchaseOrders;

namespace Consumer2.Handlers;

public abstract class IdempotentHandler<T> : IConsumer<T> where T : class, IMessage
{
    private static IInbox _inbox = new InMemoryInbox();

    public async Task Consume(ConsumeContext<T> context)
    {
        if (await _inbox.IsAlreadyProcessed(context.Message.MessageId))
        {
            // Message has already been processed; skip processing.
            return;
        }

        using (var transaction = BeginTransaction())
        {
            try
            {
                await Process(context.Message);
                await AddEventToInbox(context.Message);
                await CommitTransaction(transaction);
            }
            catch (Exception)
            {
                await RollbackTransaction(transaction);
                throw;
            }
        }
    }

    private async Task AddEventToInbox(T contextMessage)
    {
        await _inbox.AddToProcessed(contextMessage.MessageId);
    }

    protected virtual ITransaction BeginTransaction() { return new FakeTransaction(); }
    protected virtual Task CommitTransaction(ITransaction transaction) { return Task.CompletedTask; }
    protected virtual Task RollbackTransaction(ITransaction transaction) { return Task.CompletedTask; }
    protected abstract Task Process(T message);

    public class FakeTransaction : ITransaction
    {
        public void Dispose() { }
    }
    public interface ITransaction : IDisposable { }
}