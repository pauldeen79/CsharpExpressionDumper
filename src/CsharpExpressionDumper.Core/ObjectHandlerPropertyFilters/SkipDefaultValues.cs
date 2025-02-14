namespace CsharpExpressionDumper.Core.ObjectHandlerPropertyFilters;

internal class SkipDefaultValues : IObjectHandlerPropertyFilter
{
    public bool IsValid(ObjectHandlerRequest command, PropertyInfo propertyInfo)
    {
        var defaultValue = GetDefaultValue(propertyInfo);

        var actualValue = propertyInfo.GetValue(command.Instance);

        if (defaultValue is null && actualValue is null)
        {
            return false;
        }

        if (propertyInfo.PropertyType == typeof(string) && actualValue?.Equals(string.Empty) == true)
        {
            return false;
        }

        return defaultValue is null
            || actualValue is null
            || (actualValue is IEnumerable e && !e.OfType<object>().Any())
            || !actualValue.Equals(defaultValue);
    }

    private static object? GetDefaultValue(PropertyInfo propertyInfo)
    {
        var defaultValueAttribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();
        if (defaultValueAttribute is not null)
        {
            return defaultValueAttribute.Value;
        }

        return propertyInfo.PropertyType.IsValueType && Nullable.GetUnderlyingType(propertyInfo.PropertyType) is null
            ? Activator.CreateInstance(propertyInfo.PropertyType)
            : null;
    }
}
