using System;
using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.CustomTypeHandlers
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
