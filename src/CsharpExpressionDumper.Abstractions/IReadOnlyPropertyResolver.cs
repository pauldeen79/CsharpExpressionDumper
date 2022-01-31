namespace CsharpExpressionDumper.Abstractions;

public interface IReadOnlyPropertyResolver
{
    PropertyInfo Process(PropertyInfo[] properties, ConstructorInfo ctor, ParameterInfo argument);
}
