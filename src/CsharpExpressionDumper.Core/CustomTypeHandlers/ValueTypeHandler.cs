namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal class ValueTypeHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if ((request.InstanceType?.IsValueType) != true)
        {
            return false;
        }

        callback.AppendSingleValue(string.Format(CultureInfo.InvariantCulture, "{0}", request.Instance));
        return true;
    }
}
