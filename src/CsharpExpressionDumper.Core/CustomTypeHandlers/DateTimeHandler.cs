namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

public class DateTimeHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is DateTime dateTime))
        {
            return false;
        }

        callback.ChainAppendPrefix()
            .ChainAppend($"new ")
            .ChainAppendTypeName(typeof(DateTime))
            .ChainAppend($"({dateTime.Year}, {dateTime.Month}, {dateTime.Day}, {dateTime.Hour}, {dateTime.Minute}, {dateTime.Second}, {dateTime.Millisecond}, ")
            .ChainAppendTypeName(typeof(DateTimeKind))
            .ChainAppend($".{dateTime.Kind})")
            .ChainAppendSuffix();

        return true;
    }
}
