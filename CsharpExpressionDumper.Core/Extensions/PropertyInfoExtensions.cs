using System.Reflection;

namespace CsharpExpressionDumper.Core.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static bool IsReadOnly(this PropertyInfo property)
            => !property.CanWrite || property.GetSetMethod() == null;
    }
}
