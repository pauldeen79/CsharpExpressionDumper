using System;
using System.Reflection;

namespace CsharpExpressionDumper.Abstractions
{
    public interface IConstructorResolver
    {
        ConstructorInfo Resolve(Type type);
    }
}
