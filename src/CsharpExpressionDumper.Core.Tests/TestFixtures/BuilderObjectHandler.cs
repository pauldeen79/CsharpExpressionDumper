namespace CsharpExpressionDumper.Core.Tests.TestFixtures;

public class BuilderObjectHandler : IObjectHandler
{
    public bool ProcessInstance(ObjectHandlerRequest command, ICsharpExpressionDumperCallback callback)
    {
        var type = command.Type ?? command.InstanceType;
        if (type == null)
        {
            return false;
        }

        var level = command.Level + 1;
        var first = true;
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var processedProperties = new List<string>();

        foreach (var property in properties.Where(x => callback.IsPropertyValid(command, x)))
        {
            if (type.GetMethods().Any(x => x.Name == $"With{property.Name}"))
            {
                first = ProcessBuilderMethod(command, callback, level, first, processedProperties, property, "With");
            }
            else if (type.GetMethods().Any(x => x.Name == $"Add{property.Name}"))
            {
                first = ProcessBuilderMethod(command, callback, level, first, processedProperties, property, "Add");
            }
        }

        if (first)
        {
            // No 'With' or 'Add' methods found, let the default object handler handle this
            return false;
        }

        return true;
    }

    private static bool ProcessBuilderMethod(ObjectHandlerRequest command,
                                             ICsharpExpressionDumperCallback callback,
                                             int level,
                                             bool first,
                                             List<string> processedProperties,
                                             PropertyInfo property,
                                             string methodPrefix)
    {
        if (first)
        {
            callback.Append("()");
            first = false;
        }
        var propertyValue = property.GetValue(command.Instance);

        var addedSomething = false;
        if (methodPrefix == "Add"
            && !(propertyValue is string)
            && propertyValue is IEnumerable enumerable)
        {
            var firstVal = true;
            level++;
            foreach (var value in enumerable.OfType<object>())
            {
                if (firstVal)
                {
                    firstVal = false;
                    addedSomething = true;
                    callback.ChainAppendLine()
                            .ChainAppend(new string(' ', (level - 1) * 4))
                            .ChainAppend($".{methodPrefix}{property.Name}(")
                            .ChainAppendLine()
                            .ChainAppend(new string(' ', level * 4));
                }
                else
                {
                    callback.ChainAppend(",")
                            .ChainAppendLine()
                            .ChainAppend(new string(' ', level * 4));
                }
                callback.ProcessRecursive(value, value?.GetType(), level);
            }
        }
        else
        {
            addedSomething = true;
            callback.ChainAppendLine()
                    .ChainAppend(new string(' ', level * 4))
                    .ChainAppend($".{methodPrefix}{property.Name}(")
                    .ChainProcessRecursive(propertyValue, propertyValue?.GetType(), level);
        }
        if (addedSomething)
        {
            callback.Append(")");
        }
        processedProperties.Add(property.Name);
        return first;
    }
}
