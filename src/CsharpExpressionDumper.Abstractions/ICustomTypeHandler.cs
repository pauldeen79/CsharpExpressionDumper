namespace CsharpExpressionDumper.Abstractions;

public interface ICustomTypeHandler
{
    bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback);
}
