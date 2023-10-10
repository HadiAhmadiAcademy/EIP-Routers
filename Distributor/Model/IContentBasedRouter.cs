namespace Distributor.Model;

public interface IContentBasedRouter<T>
{
    string FindDestinationFor(T message);
}