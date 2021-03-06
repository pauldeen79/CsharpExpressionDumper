using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;
using CsharpExpressionDumper.Core.Extensions;
using System;

namespace CsharpExpressionDumper.Core.CustomTypeHandlers
{
    public class ValueTupleHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (request.Instance != null
                && request.InstanceType?.IsGenericType == true
                && request.InstanceType.GetGenericTypeDefinition()?.FullName.StartsWith("System.ValueTuple`") == true)
            {
                var genericArguments = request.InstanceType.GetGenericArguments();
                AppendInitialization(callback, genericArguments);

                var t = request.Instance.GetType();
                var first = true;
                for (int i = 1; i <= genericArguments.Length; i++)
                {
                    first = AppendSeparator(callback, first);
                    var item = t.GetField($"Item{i}").GetValue(request.Instance);
                    callback.ProcessRecursive(item, item?.GetType(), request.Level);
                }

                callback.ChainAppend(")")
                        .ChainAppendSuffix();

                return true;
            }

            return false;
        }

        private static bool AppendSeparator(ICsharpExpressionDumperCallback callback, bool first)
        {
            if (!first)
            {
                callback.Append(", ");
            }
            else
            {
                first = false;
            }

            return first;
        }

        private static void AppendInitialization(ICsharpExpressionDumperCallback callback, Type[] genericArguments)
        {
            callback.ChainAppendPrefix()
                    .ChainAppend("new System.ValueTuple<");

            var first = true;
            foreach (var itemType in genericArguments)
            {
                first = AppendSeparator(callback, first);
                callback.AppendTypeName(itemType);
            }

            callback.ChainAppend(">")
                    .ChainAppend("(");
        }
    }
}
