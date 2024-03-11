namespace CsharpExpressionDumper.Core;

public class StringLiteral : IStringLiteral
{
    public StringLiteral(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Value { get; }
}
