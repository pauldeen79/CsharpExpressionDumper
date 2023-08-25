namespace CsharpExpressionDumper.Core.Extensions;

internal static class PropertyInfoExtensions
{
    public static bool IsReadOnly(this PropertyInfo property)
        => !property.CanWrite || property.GetSetMethod() == null;
}
