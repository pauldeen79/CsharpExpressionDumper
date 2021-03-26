using System;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Commands;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
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
