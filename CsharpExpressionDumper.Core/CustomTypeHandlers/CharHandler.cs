using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Commands;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class CharHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is char c)
            {
                callback.AppendSingleValue($"'{c}'");
                return true;
            }

            return false;
        }
    }
}
