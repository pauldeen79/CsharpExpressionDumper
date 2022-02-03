namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

public class TimeSpanHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is TimeSpan timeSpan))
        {
            return false;
        }

        callback.ChainAppendPrefix()
                .ChainAppend($"new ")
                .ChainAppendTypeName(typeof(TimeSpan))
                .ChainAppend($"({timeSpan.Days}, {timeSpan.Hours}, {timeSpan.Minutes}, {timeSpan.Seconds}, {timeSpan.Milliseconds})")
                .ChainAppendPrefix();
        return true;
    }
}
