using System;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Extensions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class TypeHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is Type t)
            {
                callback.ChainAppendPrefix()
                        .ChainAppend("typeof(")
                        .ChainAppendTypeName(t)
                        .ChainAppend(")");

                return true;
            }

            return false;
        }
    }
}
