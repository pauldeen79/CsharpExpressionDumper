﻿namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal class StringLiteralHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is StringLiteral literal))
        {
            return false;
        }

        callback.AppendSingleValue(literal.Value);
        return true;
    }
}
