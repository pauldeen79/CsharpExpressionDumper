namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal class KeyValuePairHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (request.Instance == null
            || (request.InstanceType?.IsGenericType) != true
            || request.InstanceType.GetGenericTypeDefinition() != typeof(KeyValuePair<,>))
        {
            return false;
        }

        var genericArguments = request.InstanceType.GetGenericArguments();

        callback.ChainAppendPrefix()
                .ChainAppend("new ")
                .ChainAppendTypeName(typeof(KeyValuePair<,>))
                .ChainAppend("<")
                .ChainAppendTypeName(genericArguments[0])
                .ChainAppend(", ")
                .ChainAppendTypeName(genericArguments[1])
                .ChainAppend(">")
                .ChainAppend("(");

        var t = request.Instance.GetType();
        var key = t.GetProperty("Key").GetValue(request.Instance);
        var value = t.GetProperty("Value").GetValue(request.Instance);

        callback.ChainProcessRecursive(key, key?.GetType(), request.Level)
                .ChainAppend(", ")
                .ChainProcessRecursive(value, value?.GetType(), request.Level)
                .ChainAppend(")")
                .ChainAppendSuffix();

        return true;
    }
}
