using CsharpExpressionDumper.Abstractions.Commands;

namespace CsharpExpressionDumper.Abstractions
{
    public interface IObjectHandler
    {
        bool ProcessInstance(ObjectHandlerCommand command,
                             ICsharpExpressionDumperCallback callback);
    }
}
