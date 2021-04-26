using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;
using CsharpExpressionDumper.Core.Extensions;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class StringHandler : ICustomTypeHandler
    {
        private const string Quote = "\"";

        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (request.Instance is string stringValue)
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
