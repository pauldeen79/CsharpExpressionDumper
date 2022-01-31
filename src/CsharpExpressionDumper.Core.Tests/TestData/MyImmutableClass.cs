namespace CsharpExpressionDumper.Core.Tests.TestData;

public class MyImmutableClass
{
    public string Property1 { get; }
    public int Property2 { get; }

    public MyImmutableClass(string property1, int property2)
    {
        Property1 = property1;
        Property2 = property2;
    }
}
