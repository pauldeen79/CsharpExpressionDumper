using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class NullHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (request.Instance == null)
            {
                callback.AppendSingleValue("null");
                return true;
            }

            return false;
        }
    }
}
