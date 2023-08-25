namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal class DecimalTypeHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (request.InstanceType != typeof(decimal))
        {
            return false;
        }

        callback.AppendSingleValue(string.Format(CultureInfo.InvariantCulture, "{0}M", request.Instance));
        return true;
    }
}
