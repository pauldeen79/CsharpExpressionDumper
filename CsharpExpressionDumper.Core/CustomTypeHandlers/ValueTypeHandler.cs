using System.Globalization;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Commands;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
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
