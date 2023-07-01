namespace Router.Aggregators;

public class HandlerNotFoundException : Exception
{
    public HandlerNotFoundException(string className, string handlerName)
        : base(CreateMessage(className, handlerName))
    {
        
    }

    private static string CreateMessage(string className, string messageName)
    {
        return $"class '{className}' does not implement the interface 'IHandleAggregation<{messageName}>'";
    }
}