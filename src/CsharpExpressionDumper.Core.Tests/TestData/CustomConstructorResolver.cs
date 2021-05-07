using CsharpExpressionDumper.Abstractions;
using System;
using System.Linq;
using System.Reflection;

namespace CsharpExpressionDumper.Core.Tests.TestData
{
    public class CustomConstructorResolver : IConstructorResolver
    {
        public ConstructorInfo? Resolve(Type type)
        {
            if (type == typeof(MyImmutableClassWithTwoCtors))
            {
                return type.GetConstructors().Single(x => x.GetParameters().Length == 2);
            }

            return null;
        }
    }
}
