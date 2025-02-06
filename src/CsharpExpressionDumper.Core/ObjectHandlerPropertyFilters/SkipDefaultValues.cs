namespace CsharpExpressionDumper.Core.ObjectHandlerPropertyFilters;

internal class SkipDefaultValues : IObjectHandlerPropertyFilter
{
    public bool IsValid(ObjectHandlerRequest command, PropertyInfo propertyInfo)
    {
        var defaultValue = propertyInfo.PropertyType.IsValueType && Nullable.GetUnderlyingType(propertyInfo.PropertyType) is null
            ? Activator.CreateInstance(propertyInfo.PropertyType)
            : null;

        var actualValue = propertyInfo.GetValue(command.Instance);

        if (defaultValue is null && actualValue is null)
        {
            return false;
        }

        return defaultValue is null
            || actualValue is null
            || (actualValue is IEnumerable e && !e.OfType<object>().Any())
            || !actualValue.Equals(defaultValue);
    }
}
