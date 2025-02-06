namespace CsharpExpressionDumper.Core.Tests.TestData;

public class MyClassWithReservedKeywordArgument
{
    public MyClassWithReservedKeywordArgument(string @namespace)
    {
        Namespace = @namespace;
    }

    public string Namespace { get; }
}
