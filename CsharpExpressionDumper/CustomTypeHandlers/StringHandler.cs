using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Extensions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class StringHandler : ICustomTypeHandler
    {
        private const string Quote = "\"";

        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is string stringValue)
            {
                callback.ChainAppendPrefix()
                        .ChainAppend('@')
                        .ChainAppend(Quote)
                        .ChainAppend(Format(stringValue))
                        .ChainAppend(Quote)
                        .ChainAppendSuffix();

                return true;
            }

            return false;
        }

        private static string Format(string stringValue)
            => stringValue.Replace("\"", "\"\"");
    }
}
