using System;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class GuidHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (request.Instance is Guid guid)
            {
                callback.AppendSingleValue($@"new System.Guid(""{guid}"")");
                return true;
            }

            return false;
        }
    }
}
