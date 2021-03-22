using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Extensions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class DictionaryHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.InstanceType?.IsGenericType == true
                && new[] { typeof(IDictionary<,>), typeof(Dictionary<,>) }.Contains(command.InstanceType.GetGenericTypeDefinition()))
            {
                var genericArguments = command.InstanceType.GetGenericArguments();
                if (genericArguments.Length != 2)
                {
                    return false;
                }

                callback.ChainAppendPrefix()
                        .ChainAppend("new System.Collections.Generic.Dictionary<")
                        .ChainAppendTypeName(genericArguments[0])
                        .ChainAppend(", ")
                        .ChainAppendTypeName(genericArguments[1])
                        .ChainAppendLine(">")
                        .ChainAppend(new string(' ', command.Level * 4))
                        .ChainAppendLine("{");

                var level = command.Level + 1;
                if (command.Instance is IEnumerable enumerable)
                {
                    foreach (var kvp in enumerable)
                    {
                        var t = kvp.GetType();
                        var key = t.GetProperty("Key").GetValue(kvp);
                        var value = t.GetProperty("Value").GetValue(kvp);
                        callback.ChainAppend(new string(' ', level * 4))
                                .ChainAppend("[")
                                .ChainProcessRecursive(key, key?.GetType(), level)
                                .ChainAppend("] = ")
                                .ChainProcessRecursive(value, value?.GetType(), level)
                                .ChainAppendLine(",");
                    }
                }

                level--;

                callback.ChainAppend(new string(' ', level * 4))
                        .ChainAppend("}")
                        .ChainAppendSuffix();

                return true;
            }

            return false;
        }
    }
}
