namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal class BooleanHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is bool b))
        {
            return false;
        }

        callback.AppendSingleValue(DisplayBoolean(b));
        return true;
    }

    private static string DisplayBoolean(bool booleanValue)
        => booleanValue
            ? "true"
            : "false";
}
