using System;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class VersionHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (!(request.Instance is Version version))
            {
                return false;
            }
            
            callback.AppendSingleValue($"new System.Version({version.Major}, {version.Minor}, {version.Build}, {version.Revision})");
            return true;
        }
    }
}
