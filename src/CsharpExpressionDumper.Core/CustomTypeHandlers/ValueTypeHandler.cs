using System.Globalization;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class ValueTypeHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (request.InstanceType?.IsValueType == true)
            {
                callback.AppendSingleValue(string.Format(CultureInfo.InvariantCulture, "{0}", request.Instance));
                return true;
            }

            return false;
        }
    }
}
