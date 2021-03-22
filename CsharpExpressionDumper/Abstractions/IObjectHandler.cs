using System;

namespace CsharpExpressionDumper.Abstractions
{
    public interface IObjectHandler
    {
        bool ProcessInstance(ObjectHandlerCommand command,
                             ICsharpExpressionDumperCallback callback);
    }
}
