namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal class CharHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is char c))
        {
            return false;
        }

        callback.AppendSingleValue($"'{c}'");
        return true;
    }
}
