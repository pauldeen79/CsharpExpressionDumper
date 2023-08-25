namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal  class NullHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (request.Instance is not null)
        {
            return false;
        }

        callback.AppendSingleValue("null");
        return true;
    }
}
