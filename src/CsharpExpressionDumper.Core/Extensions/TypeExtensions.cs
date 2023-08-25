namespace CsharpExpressionDumper.Core.Extensions;

internal static class TypeExtensions
{
    public static bool IsAnonymousType(this Type instance)
        => instance.FullName.Contains("AnonymousType");
}
