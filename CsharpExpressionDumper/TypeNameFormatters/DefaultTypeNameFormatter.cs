using System;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Extensions;

namespace CsharpExpressionDumper.TypeNameFormatters
{
    public class DefaultTypeNameFormatter : ITypeNameFormatter
    {
        public string Format(Type type)
        {
            return type?.FullName.FixTypeName();
        }
    }
}
