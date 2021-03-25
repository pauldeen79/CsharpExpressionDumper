using System;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Commands;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
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
