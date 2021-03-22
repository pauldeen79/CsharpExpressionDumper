namespace CsharpExpressionDumper.Abstractions
{
    public interface ICustomTypeHandler
    {
        bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback);
    }
}