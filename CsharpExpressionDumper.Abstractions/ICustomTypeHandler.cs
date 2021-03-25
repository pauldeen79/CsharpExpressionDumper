using CsharpExpressionDumper.Abstractions.Commands;

namespace CsharpExpressionDumper.Abstractions
{
    public interface ICustomTypeHandler
    {
        bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback);
    }
}