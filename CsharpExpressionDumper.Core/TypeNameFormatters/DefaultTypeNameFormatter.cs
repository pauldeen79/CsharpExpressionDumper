using System;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Core.Extensions;

namespace CsharpExpressionDumper.Core.TypeNameFormatters
{
    public class DefaultTypeNameFormatter : ITypeNameFormatter
    {
        public string Format(Type type)
        {
            return type?.FullName.FixTypeName();
        }
    }
}
