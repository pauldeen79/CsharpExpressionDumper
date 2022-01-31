namespace CsharpExpressionDumper.Core.Tests.TestData;

public class MyHalfImmutableClass2
{
    public string Property1 { get; set; }
    public int Property2 { get; set; }

    public MyHalfImmutableClass2(string property1) => Property1 = property1;
}
