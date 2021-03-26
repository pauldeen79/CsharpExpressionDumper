using System;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Commands;
using CsharpExpressionDumper.Core.Extensions;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
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
