using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Extensions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class EnumerableHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is IEnumerable enumerable)
            {
                var items = enumerable.Cast<object>().ToArray();
                var typeSuffix = GetTypeSuffix(items, command.Instance);
                if (IsGenericCollection(command.InstanceType))
                {
                    AppendCustomInitialization(command, callback, typeSuffix, "System.Collections.ObjectModel.Collection");
                }
                else if (IsGenericReadOnlyCollection(command.InstanceType))
                {
                    AppendCustomInitialization(command, callback, typeSuffix, "System.Collections.ObjectModel.ReadOnlyCollection");
                }
                else if (IsGenericList(command.InstanceType))
                {
                    AppendCustomInitialization(command, callback, typeSuffix, "System.Collections.Generic.List");
                }
                else
                {
                    callback.ChainAppendPrefix()
                            .ChainAppend("new");

                    if (typeSuffix != null)
                    {
                        callback.ChainAppend(" ")
                                .ChainAppendTypeName(typeSuffix);
                    }
                    callback.AppendLine("[]");
                }
                callback.ChainAppend(new string(' ', command.Level * 4))
                        .ChainAppendLine("{");
                var level = command.Level + 1;
                foreach (var item in items)
                {
                    callback.ChainAppend(new string(' ', level * 4))
                            .ChainProcessRecursive(item, item.GetType(), level)
                            .ChainAppendLine(",");
                }
                level--;
                callback.Append(new string(' ', level * 4));
                if (IsGenericCollection(command.InstanceType)
                    || IsGenericReadOnlyCollection(command.InstanceType)
                    || IsGenericList(command.InstanceType))
                {
                    callback.Append("} )");
                }
                else
                {
                    callback.Append("}");
                }
                callback.AppendSuffix();
                return true;
            }

            return false;
        }

        private Type GetTypeSuffix(object[] items, object instance)
        {
            if (items == null || instance == null)
            {
                return null;
            }

            if (items.Length == 0 || items.Select(x => x.GetType()).Distinct().Count() > 1)
            {
                var instanceType = instance.GetType();
                if (instanceType.IsGenericType && new[]
                {
                    typeof(IEnumerable<>),
                    typeof(ICollection<>),
                    typeof(IReadOnlyCollection<>),
                    typeof(Collection<>),
                    typeof(List<>),
                    typeof(ReadOnlyCollection<>),
                    typeof(ObservableCollection<>)
                }.Contains(instanceType.GetGenericTypeDefinition()))
                {
                    var instanceGenericTypeArguments = instanceType.GetGenericArguments();
                    if (instanceGenericTypeArguments.Length != 1)
                    {
                        return null;
                    }
                    return instanceGenericTypeArguments[0];
                }

                if (instanceType.IsArray)
                {
                    var genericEnumerableType = Array.Find(instanceType.GetInterfaces(), t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
                    if (genericEnumerableType == null)
                    {
                        return null;
                    }
                    return genericEnumerableType.GetGenericArguments()[0];
                }
            }

            // If all items are the same type, then C# can infer the type without any problem
            return null;
        }

        private void AppendCustomInitialization(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback, Type typeSuffix, string collectionTypeName)
        {
            callback.ChainAppendPrefix()
                    .ChainAppend("new ")
                    .ChainAppend(collectionTypeName)
                    .ChainAppend('<')
                    .ChainAppendTypeName(command.InstanceType.GetGenericArguments()[0])
                    .ChainAppend(">(new");

            if (typeSuffix != null)
            {
                callback.ChainAppend(" ")
                        .ChainAppendTypeName(typeSuffix);
            }
            callback.AppendLine("[]");
        }

        private static bool IsGenericCollection(Type t)
            => t.IsGenericType
                && t.GetGenericTypeDefinition() == typeof(Collection<>);

        private static bool IsGenericReadOnlyCollection(Type t)
            => t.IsGenericType
                && t.GetGenericTypeDefinition() == typeof(ReadOnlyCollection<>);

        private static bool IsGenericList(Type t)
            => t.IsGenericType
                && t.GetGenericTypeDefinition() == typeof(List<>);
    }
}
