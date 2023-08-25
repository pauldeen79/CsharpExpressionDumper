namespace CsharpExpressionDumper.Core.ObjectHandlers;

internal class DefaultObjectHandler : IObjectHandler
{
    public bool ProcessInstance(ObjectHandlerRequest command, ICsharpExpressionDumperCallback callback)
    {
        if (command.Instance is null)
        {
            return false;
        }

        var type = command.Type ?? command.InstanceType;
        if (type is null)
        {
            return false;
        }

        var level = command.Level + 1;
        var first = true;
        var ctor = callback.ResolveConstructor(type);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var processedProperties = new List<string>();
        first = AppendReadOnlyProperties(command, callback, level, first, ctor, properties, processedProperties);

        level--;

        AppendFinalize(command, callback, level, first, properties, processedProperties);

        return true;
    }

    private static bool AppendInitialization(ICsharpExpressionDumperCallback callback,
                                             int level,
                                             bool first)
    {
        if (first)
        {
            first = false;
            callback.ChainAppendLine()
                    .ChainAppend(new string(' ', (level - 1) * 4))
                    .ChainAppendLine("(");
        }
        else
        {
            callback.AppendLine(",");
        }

        return first;
    }

    private static void AppendFinalize(ObjectHandlerRequest command,
                                       ICsharpExpressionDumperCallback callback,
                                       int level,
                                       bool first,
                                       PropertyInfo[] properties,
                                       List<string> processedProperties)
    {
        var writableProperties = properties.Where
        (
            property =>
                (command.IsAnonymousType || !property.IsReadOnly())
                && !processedProperties.Contains(property.Name)
                && callback.IsPropertyValid(command, property)
        ).ToArray();

        if (!first)
        {
            callback.ChainAppend(new string(' ', level * 4))
                    .ChainAppend(")");
        }
        else if (!command.IsAnonymousType && writableProperties.Length == 0)
        {
            callback.Append("()");
        }

        if (writableProperties.Length > 0)
        {
            command.ProcessWritableProperties(callback, writableProperties);
        }
    }

    private static bool AppendReadOnlyProperties(ObjectHandlerRequest command,
                                                 ICsharpExpressionDumperCallback callback,
                                                 int level,
                                                 bool first,
                                                 ConstructorInfo? ctor,
                                                 PropertyInfo[] properties,
                                                 List<string> processedProperties)
    {
        if (!command.IsAnonymousType && ctor is not null)
        {
            var arguments = ctor.GetParameters();
            if (arguments.Length > 0)
            {
                foreach (var argument in arguments)
                {
                    var readOnlyProperty = callback.ResolveReadOnlyProperty(properties, ctor, argument);
                    if (readOnlyProperty is null)
                    {
                        continue;
                    }

                    first = AppendInitialization(callback, level, first);

                    processedProperties.Add(readOnlyProperty.Name);

                    var value = readOnlyProperty.GetValue(command.Instance);

                    callback.ChainAppend(new string(' ', level * 4))
                            .ChainAppend(argument.Name)
                            .ChainAppend(": ")
                            .ChainProcessRecursive(value, value?.GetType(), level);
                }

                if (!first)
                {
                    callback.AppendLine();
                }
            }
        }

        return first;
    }
}
