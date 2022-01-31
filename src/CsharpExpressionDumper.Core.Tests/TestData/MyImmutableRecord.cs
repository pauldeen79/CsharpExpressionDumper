namespace CsharpExpressionDumper.Core.Tests.TestData;

public record MyImmutableRecord
{
    public string Property1 { get; }
    public int Property2 { get; }

    public MyImmutableRecord(string property1, int property2)
    {
        Property1 = property1;
        Property2 = property2;
    }
}
