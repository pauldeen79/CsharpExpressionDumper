using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Extensions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class ValueTupleHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.InstanceType?.IsGenericType == true
                && command.InstanceType.GetGenericTypeDefinition()?.FullName.StartsWith("System.ValueTuple`") == true)
            {
                var genericArguments = command.InstanceType.GetGenericArguments();
                if (genericArguments.Length < 2)
                {
                    return false;
                }

                callback.ChainAppendPrefix()
                        .ChainAppend("new System.ValueTuple<");

                var first = true;
                foreach (var itemType in genericArguments)
                {
                    if (!first)
                    {
                        callback.Append(", ");
                    }
                    else
                    {
                        first = false;
                    }
                    callback.AppendTypeName(itemType);
                }

                callback.ChainAppend(">")
                        .ChainAppend("(");

                var t = command.Instance.GetType();
                first = true;
                for (int i = 1; i <= genericArguments.Length; i++)
                {
                    var item = t.GetField($"Item{i}").GetValue(command.Instance);

                    if (!first)
                    {
                        callback.Append(", ");
                    }
                    else
                    {
                        first = false;
                    }
                    callback.ProcessRecursive(item, item?.GetType(), command.Level);
                }

                callback.ChainAppend(")")
                        .ChainAppendSuffix();

                return true;
            }

            return false;
        }
    }
}
