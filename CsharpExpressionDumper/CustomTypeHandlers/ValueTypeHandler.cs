using System.Globalization;
using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class ValueTypeHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.InstanceType?.IsValueType == true)
            {
                callback.AppendSingleValue(string.Format(CultureInfo.InvariantCulture, "{0}", command.Instance));
                return true;
            }

            return false;
        }
    }
}
