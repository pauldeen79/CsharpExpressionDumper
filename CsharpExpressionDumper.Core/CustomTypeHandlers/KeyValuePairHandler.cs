using System.Collections.Generic;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Commands;
using CsharpExpressionDumper.Core.Extensions;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class KeyValuePairHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.InstanceType?.IsGenericType == true
                && command.InstanceType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                var genericArguments = command.InstanceType.GetGenericArguments();
                if (genericArguments.Length != 2)
                {
                    return false;
                }

                callback.ChainAppendPrefix()
                        .ChainAppend("new System.Collections.Generic.KeyValuePair<")
                        .ChainAppendTypeName(genericArguments[0])
                        .ChainAppend(", ")
                        .ChainAppendTypeName(genericArguments[1])
                        .ChainAppend(">")
                        .ChainAppend("(");

                var t = command.Instance.GetType();
                var key = t.GetProperty("Key").GetValue(command.Instance);
                var value = t.GetProperty("Value").GetValue(command.Instance);

                callback.ChainProcessRecursive(key, key?.GetType(), command.Level)
                        .ChainAppend(", ")
                        .ChainProcessRecursive(value, value?.GetType(), command.Level)
                        .ChainAppend(")")
                        .ChainAppendSuffix();

                return true;
            }

            return false;
        }
    }
}
