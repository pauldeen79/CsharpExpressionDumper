using System;
using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class GuidHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is Guid guid)
            {
                callback.AppendSingleValue($@"new System.Guid(""{guid}"")");
                return true;
            }

            return false;
        }
    }
}
