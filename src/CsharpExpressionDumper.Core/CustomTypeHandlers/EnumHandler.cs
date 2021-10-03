using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;
using CsharpExpressionDumper.Core.Extensions;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class EnumHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (request.Instance == null
                || (request.InstanceType?.IsEnum) != true)
            {
                return false;
            }

            callback.ChainAppendPrefix()
                    .ChainAppendTypeName(request.InstanceType)
                    .ChainAppend('.')
                    .ChainAppend(request.Instance)
                    .ChainAppendSuffix();
            return true;
        }
    }
}
