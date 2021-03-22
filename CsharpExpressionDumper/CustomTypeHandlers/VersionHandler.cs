using System;
using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class VersionHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is Version version)
            {
                callback.AppendSingleValue($"new System.Version({version.Major}, {version.Minor}, {version.Build}, {version.Revision})");
                return true;
            }

            return false;
        }
    }
}
