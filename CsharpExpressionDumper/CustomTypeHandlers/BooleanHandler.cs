﻿using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class BooleanHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is bool b)
            {
                callback.AppendSingleValue(DisplayBoolean(b));
                return true;
            }

            return false;
        }

        private static string DisplayBoolean(bool booleanValue)
            => booleanValue
                ? "true"
                : "false";
    }
}
