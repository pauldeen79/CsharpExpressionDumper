﻿namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal class TypeHandler : ICustomTypeHandler
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
