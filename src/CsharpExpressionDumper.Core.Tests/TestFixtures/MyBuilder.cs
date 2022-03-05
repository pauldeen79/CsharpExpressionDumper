namespace CsharpExpressionDumper.Core.Tests.TestFixtures;

public class MyBuilder
{
    public string Name { get; set; } = string.Empty;
    public List<string> Values { get; set; } = new List<string>();

    public MyBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public MyBuilder AddValues(params string[] values)
    {
        Values.AddRange(values);
        return this;
    }
}
