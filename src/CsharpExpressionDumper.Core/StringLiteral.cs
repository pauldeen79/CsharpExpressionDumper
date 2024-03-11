namespace CsharpExpressionDumper.Core;

public class StringLiteral
{
    public StringLiteral(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Value { get; }
}
