using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Commands;
using CsharpExpressionDumper.Core.Extensions;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class EnumHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.InstanceType?.IsEnum == true)
            {
                callback.ChainAppendPrefix()
                        .ChainAppendTypeName(command.InstanceType)
                        .ChainAppend('.')
                        .ChainAppend(command.Instance)
                        .ChainAppendSuffix();
                return true;
            }

            return false;
        }
    }
}
