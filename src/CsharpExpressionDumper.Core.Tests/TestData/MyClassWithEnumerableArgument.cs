namespace CsharpExpressionDumper.Core.Tests.TestData;

public class MyClassWithEnumerableArgument
{
    public MyClassWithEnumerableArgument(IEnumerable<string> values)
    {
        Values = values.ToList();
    }

    public List<string> Values { get; }
}
