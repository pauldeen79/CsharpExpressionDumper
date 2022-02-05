namespace CsharpExpressionDumper.Core.Tests.TestData;

public class MyCustomTypeFormatter : ITypeNameFormatter
{
    public string? Format(string currentValue) => currentValue.GetClassName().ToUpperInvariant();
}
