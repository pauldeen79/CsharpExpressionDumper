using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;
using CsharpExpressionDumper.Core.Extensions;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class DictionaryHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (request.InstanceType?.IsGenericType == true
                && new[] { typeof(IDictionary<,>), typeof(Dictionary<,>) }.Contains(request.InstanceType.GetGenericTypeDefinition()))
            {
                var genericArguments = request.InstanceType.GetGenericArguments();

                callback.ChainAppendPrefix()
                        .ChainAppend("new System.Collections.Generic.Dictionary<")
                        .ChainAppendTypeName(genericArguments[0])
                        .ChainAppend(", ")
                        .ChainAppendTypeName(genericArguments[1])
                        .ChainAppendLine(">")
                        .ChainAppend(new string(' ', request.Level * 4))
                        .ChainAppendLine("{");

                var level = request.Level + 1;
                if (request.Instance is IEnumerable enumerable)
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
