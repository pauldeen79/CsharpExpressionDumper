namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal class SingleTypeHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (request.InstanceType != typeof(float))
        {
            return false;
        }

        callback.AppendSingleValue(string.Format(CultureInfo.InvariantCulture, "{0}F", request.Instance));
        return true;
    }
}
