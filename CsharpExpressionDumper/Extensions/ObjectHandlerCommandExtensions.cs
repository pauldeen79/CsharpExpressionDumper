using System.Collections.Generic;
using System.Reflection;
using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.Extensions
{
    public static class ObjectHandlerCommandExtensions
    {
        public static void ProcessWritableProperties(this ObjectHandlerCommand command,
                                                     ICsharpExpressionDumperCallback callback,
                                                     IEnumerable<PropertyInfo> properties)
        {
            callback.ChainAppendLine()
                    .ChainAppend(new string(' ', command.Level * 4))
                    .ChainAppendLine("{");

            var level = command.Level + 1;

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(command.Instance);
                var propertyType = propertyValue?.GetType();
                callback.Append(new string(' ', level * 4));
                var propertyCommand = new CustomTypeHandlerCommand(propertyValue, propertyType, level);
                var propertyIsCustom = callback.IsPropertyCustom(propertyCommand, $"{property.Name} = ", ",");
                if (!propertyIsCustom)
                {
                    callback.ChainAppend(property.Name)
                            .ChainAppend(" = ")
                            .ChainProcessRecursive(propertyValue, propertyValue?.GetType(), level)
                            .ChainAppend(",");
                }

                callback.AppendLine();
            }

            level--;

            callback.ChainAppend(new string(' ', level * 4))
                    .ChainAppend("}");
        }
    }
}
