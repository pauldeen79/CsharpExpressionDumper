using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class CharHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (request.Instance is char c)
            {
                callback.AppendSingleValue($"'{c}'");
                return true;
            }

            return false;
        }
    }
}
