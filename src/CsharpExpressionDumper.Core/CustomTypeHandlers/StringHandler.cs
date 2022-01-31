namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

public class StringHandler : ICustomTypeHandler
{
    private const string Quote = "\"";

    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is string stringValue))
        {
            return false;
        }

        callback.ChainAppendPrefix()
                .ChainAppend('@')
                .ChainAppend(Quote)
                .ChainAppend(Format(stringValue))
                .ChainAppend(Quote)
                .ChainAppendSuffix();

        return true;
    }

    private static string Format(string stringValue)
        => stringValue.Replace("\"", "\"\"");
}
