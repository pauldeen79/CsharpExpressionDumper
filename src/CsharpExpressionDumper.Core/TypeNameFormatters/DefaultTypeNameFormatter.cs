namespace CsharpExpressionDumper.Core.TypeNameFormatters;

public class DefaultTypeNameFormatter : ITypeNameFormatter
{
    public string Format(Type type) => type.FullName.FixTypeName();
}
