namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal class GuidHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is Guid guid))
        {
            return false;
        }

        callback.ChainAppendPrefix()
                .ChainAppend($"new ")
                .ChainAppendTypeName(typeof(Guid))
                .ChainAppend("(\"")
                .ChainAppend(guid)
                .ChainAppend("\")")
                .ChainAppendSuffix();
        return true;
    }
}
