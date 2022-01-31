namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

public class GuidHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is Guid guid))
        {
            return false;
        }

        callback.AppendSingleValue($@"new System.Guid(""{guid}"")");
        return true;
    }
}
