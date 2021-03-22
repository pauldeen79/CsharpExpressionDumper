using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class NullHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance == null)
            {
                callback.AppendSingleValue("null");
                return true;
            }

            return false;
        }
    }
}
