namespace CsharpExpressionDumper.Core.Tests.TestData;

public class MyHalfImmutableClass1
{
    public string Property1 { get; }
    public int Property2 { get; set; }

    public MyHalfImmutableClass1(string property1) => Property1 = property1;
}
