using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class BooleanHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (request.Instance is bool b)
            {
                callback.AppendSingleValue(DisplayBoolean(b));
                return true;
            }

            return false;
        }

        private static string DisplayBoolean(bool booleanValue)
            => booleanValue
                ? "true"
                : "false";
    }
}
