namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal class UriHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is Uri uri))
        {
            return false;
        }

        callback.ChainAppendPrefix()
                .ChainAppend("new ")
                .ChainAppendTypeName(typeof(Uri))
                .ChainAppend($@"(""{uri.AbsoluteUri}"")")
                .ChainAppendSuffix();
        return true;
    }
}
