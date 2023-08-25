namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal  class NullHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (request.Instance != null)
        {
            return false;
        }

        callback.AppendSingleValue("null");
        return true;
    }
}
