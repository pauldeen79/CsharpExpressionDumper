using System;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Commands;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class UriHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is Uri uri)
            {
                callback.AppendSingleValue($@"new System.Uri(""{uri.AbsoluteUri}"")");
                return true;
            }

            return false;
        }
    }
}
