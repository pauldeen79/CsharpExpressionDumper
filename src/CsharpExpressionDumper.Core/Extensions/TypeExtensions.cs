using System;

namespace CsharpExpressionDumper.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAnonymousType(this Type instance)
            => instance?.FullName.Contains("AnonymousType") == true;
    }
}
