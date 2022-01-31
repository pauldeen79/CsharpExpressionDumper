namespace CsharpExpressionDumper.Core.ObjectHandlerPropertyFilters;

public class SkipDefaultValues : IObjectHandlerPropertyFilter
{
    public bool IsValid(ObjectHandlerRequest command, PropertyInfo propertyInfo)
    {
        var defaultValue = propertyInfo.PropertyType.IsValueType && Nullable.GetUnderlyingType(propertyInfo.PropertyType) == null
            ? Activator.CreateInstance(propertyInfo.PropertyType)
            : null;

        var actualValue = propertyInfo.GetValue(command.Instance);

        if (defaultValue == null && actualValue == null)
        {
            return false;
        }

        return defaultValue == null
            || actualValue == null
            || !actualValue.Equals(defaultValue);
    }
}
