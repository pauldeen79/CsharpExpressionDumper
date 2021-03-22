using System;
using System.Reflection;
using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.ConstructorResolvers
{
    public class DefaultConstructorResolver : IConstructorResolver
    {
        public ConstructorInfo Resolve(Type type)
        {
            var ctors = type.GetConstructors();
            if (ctors.Length == 1)
            {
                return ctors[0];
            }

            return null;
        }
    }
}
