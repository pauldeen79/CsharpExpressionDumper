using System;
using System.Reflection;
using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.ReadOnlyPropertyResolvers
{
    public class DefaultReadOnlyPropertyResolver : IReadOnlyPropertyResolver
    {
        public PropertyInfo Process(PropertyInfo[] properties, ConstructorInfo ctor, ParameterInfo argument)
            => Array.Find
            (
                properties,
                property => property.Name.Equals(argument.Name, StringComparison.InvariantCultureIgnoreCase)
            );
    }
}
