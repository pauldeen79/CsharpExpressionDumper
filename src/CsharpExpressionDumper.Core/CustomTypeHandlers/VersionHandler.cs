namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

public class VersionHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is Version version))
        {
            return false;
        }

        callback.ChainAppendPrefix()
                .ChainAppend("new ")
                .ChainAppendTypeName(typeof(Version))
                .ChainAppend($"({version.Major}, {version.Minor}, {version.Build}, {version.Revision})")
                .ChainAppendSuffix();
        return true;
    }
}
