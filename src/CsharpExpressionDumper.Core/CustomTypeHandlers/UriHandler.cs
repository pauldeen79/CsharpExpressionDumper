namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

public class UriHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is Uri uri))
        {
            return false;
        }

        callback.AppendSingleValue($@"new System.Uri(""{uri.AbsoluteUri}"")");
        return true;
    }
}
