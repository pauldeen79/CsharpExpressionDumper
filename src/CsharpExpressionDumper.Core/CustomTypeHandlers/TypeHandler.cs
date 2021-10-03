using System;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;
using CsharpExpressionDumper.Core.Extensions;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class TypeHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (!(request.Instance is Type t))
            {
                return false;
            }
            
            callback.ChainAppendPrefix()
                    .ChainAppend("typeof(")
                    .ChainAppendTypeName(t)
                    .ChainAppend(")");

            return true;
        }
    }
}
