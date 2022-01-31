namespace CsharpExpressionDumper.Abstractions;

public interface IConstructorResolver
{
    ConstructorInfo? Resolve(Type type);
}
