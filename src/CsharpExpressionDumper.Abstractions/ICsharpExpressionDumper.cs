namespace CsharpExpressionDumper.Abstractions;

public interface ICsharpExpressionDumper
{
    string Dump(object? instance, Type? type = null);
}
