using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Extensions;

namespace CsharpExpressionDumper.ObjectHandlers
{
    public class DefaultObjectHandler : IObjectHandler
    {
        public bool ProcessInstance(ObjectHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance == null)
            {
                return false;
            }

            var level = command.Level + 1;
            var first = true;
            var type = command.Type ?? command.InstanceType;
            if (type == null)
            {
                return false;
            }

            var ctor = callback.ResolveConstructor(type);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var processedProperties = new List<string>();
            if (!command.IsAnonymousType && ctor != null)
            {
                var arguments = ctor.GetParameters();
                if (arguments.Length > 0)
                {
                    foreach (var argument in arguments)
                    {
                        var readOnlyProperty = callback.ResolveReadOnlyProperty(properties, ctor, argument);
                        if (readOnlyProperty == null)
                        {
                            continue;
                        }

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

            level--;

            var writableProperties = properties.Where
            (
                property =>
                    (command.IsAnonymousType || !property.IsReadOnly())
                    && !processedProperties.Contains(property.Name)
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

            return true;
        }
    }
}
