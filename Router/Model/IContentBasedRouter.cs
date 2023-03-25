namespace Router.Model;

public interface IContentBasedRouter<T>
{
    string FindDestinationFor(T message);
}