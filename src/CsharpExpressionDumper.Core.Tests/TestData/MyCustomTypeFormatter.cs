namespace CsharpExpressionDumper.Core.Tests.TestData;

public class MyCustomTypeFormatter : ITypeNameFormatter
{
    public string? Format(Type type) => type.Name.ToUpperInvariant(); //note that the default implementation gives the fullname, so we're fine here
}
