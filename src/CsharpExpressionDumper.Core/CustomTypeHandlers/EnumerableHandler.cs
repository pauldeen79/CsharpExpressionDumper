namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

internal class EnumerableHandler : ICustomTypeHandler
{
    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is IEnumerable enumerable)
            || request.InstanceType == null)
        {
            return false;
        }

        var items = enumerable.Cast<object>().ToArray();
        var typeSuffix = GetTypeSuffix(items, request.Instance);
        AppendInitialization(request, callback, request.InstanceType, typeSuffix, items.Any());
        var level = request.Level + 1;
        foreach (var item in items)
        {
            callback.ChainAppend(new string(' ', level * 4))
                    .ChainProcessRecursive(item, item.GetType(), level)
                    .ChainAppendLine(",");
        }
        level--;
        callback.Append(new string(' ', level * 4));
        if (TypeIsGenericSequence(request.InstanceType))
        {
            if (items.Any())
            {
                callback.Append("} )");
            }
            else
            {
                callback.Append(")");
            }
        }
        else
        {
            callback.Append("}");
        }
        callback.AppendSuffix();
        return true;
    }

    private Type? GetTypeSuffix(object[] items, object instance)
    {
        if (TypeIsEmpty(items, instance))
        {
            return null;
        }

        if (ItemsAreOfTheSameType(items))
        {
            // If all items are the same type, then C# can infer the type without any problem
            return null;
        }

        var instanceType = instance.GetType();
        if (TypeIsGenericSequence(instanceType))
        {
            var instanceGenericTypeArguments = instanceType.GetGenericArguments();
            return instanceGenericTypeArguments[0];
        }

        if (instanceType.IsArray)
        {
            var genericEnumerableType = GetEnumerableGenericArgumentType(instanceType);
            if (genericEnumerableType == null)
            {
                return null;
            }
            return genericEnumerableType.GetGenericArguments()[0];
        }

        // If all items are the same type, then C# can infer the type without any problem
        return null;
    }

    private void AppendInitialization(CustomTypeHandlerRequest request,
                                      ICsharpExpressionDumperCallback callback,
                                      Type instanceType,
                                      Type? typeSuffix,
                                      bool hasItems)
    {
        if (TypeIsGenericSequence(request.InstanceType))
        {
            AppendCustomInitialization(request, callback, typeSuffix, instanceType.GetGenericTypeDefinition(), hasItems);
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

        if (hasItems || !TypeIsGenericSequence(request.InstanceType))
        {
            callback.ChainAppend(new string(' ', request.Level * 4))
                    .ChainAppendLine("{");
        }
    }

    private void AppendCustomInitialization(CustomTypeHandlerRequest request,
                                            ICsharpExpressionDumperCallback callback,
                                            Type? typeSuffix,
                                            Type collectionType,
                                            bool hasItems)
    {
        callback.ChainAppendPrefix()
                .ChainAppend("new ")
                .ChainAppendTypeName(collectionType)
                .ChainAppend('<')
                .ChainAppendTypeName(request.InstanceType!.GetGenericArguments()[0])
                .ChainAppend(hasItems ? ">(new" : ">(");
        if (hasItems)
        {
            if (typeSuffix != null)
            {
                callback.ChainAppend(" ")
                        .ChainAppendTypeName(typeSuffix);
            }
            callback.AppendLine("[]");
        }
    }

    private static Type GetEnumerableGenericArgumentType(Type instanceType)
        => Array.Find
        (
            instanceType.GetInterfaces(),
            t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>)
        );

    private static bool TypeIsEmpty(object[] items, object instance)
        => items == null || instance == null;

    private static bool ItemsAreOfTheSameType(object[] items)
        => items.Length != 0
            && items.Select(x => x.GetType()).Distinct().Count() <= 1;

    private static readonly string[] _types = new[]
         {
            "Enumerable`",
            "Collection`",
            "List`",
            "Array`"
         };

    private static bool TypeIsGenericSequence(Type? instanceType)
         => instanceType != null && instanceType.IsGenericType && Array.Exists(_types, x => instanceType.GetGenericTypeDefinition().FullName.Contains(x));
}
