namespace CsharpExpressionDumper.Core.CustomTypeHandlers;

public class EnumerableHandler : ICustomTypeHandler
{
    private readonly IEnumerable<ITypeNameFormatter> _typeNameFormatters;

    public EnumerableHandler(IEnumerable<ITypeNameFormatter> typeNameFormatters) => _typeNameFormatters = typeNameFormatters;

    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (!(request.Instance is IEnumerable enumerable)
            || request.InstanceType == null)
        {
            return false;
        }

        var items = enumerable.Cast<object>().ToArray();
        var typeSuffix = GetTypeSuffix(items, request.Instance);
        AppendInitialization(request, callback, typeSuffix);
        var level = request.Level + 1;
        foreach (var item in items)
        {
            callback.ChainAppend(new string(' ', level * 4))
                    .ChainProcessRecursive(item, item.GetType(), level)
                    .ChainAppendLine(",");
        }
        level--;
        callback.Append(new string(' ', level * 4));
        if (IsGenericCollectionOrDerrivedType(request))
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
                                      Type? typeSuffix)
    {
        if (IsGenericCollection(request.InstanceType))
        {
            AppendCustomInitialization(request, callback, typeSuffix, GetCollectionTypeName(typeof(Collection<>)));
        }
        else if (IsGenericReadOnlyCollection(request.InstanceType))
        {
            AppendCustomInitialization(request, callback, typeSuffix, GetCollectionTypeName(typeof(ReadOnlyCollection<>)));
        }
        else if (IsGenericList(request.InstanceType))
        {
            AppendCustomInitialization(request, callback, typeSuffix, GetCollectionTypeName(typeof(List<>)));
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
        callback.ChainAppend(new string(' ', request.Level * 4))
                .ChainAppendLine("{");
    }

    private string GetCollectionTypeName(Type type)
        => _typeNameFormatters.ProcessUntilSuccess(x => x.Format(type)) ?? type.FullName.FixTypeName();

    private void AppendCustomInitialization(CustomTypeHandlerRequest request,
                                            ICsharpExpressionDumperCallback callback,
                                            Type? typeSuffix,
                                            string collectionTypeName)
    {
        callback.ChainAppendPrefix()
                .ChainAppend("new ")
                .ChainAppend(collectionTypeName)
                .ChainAppend('<')
#pragma warning disable CS8602 // Dereference of a possibly null reference. False positive, this has already been checked in the public method above.
                .ChainAppendTypeName(request.InstanceType.GetGenericArguments()[0])
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                .ChainAppend(">(new");

        if (typeSuffix != null)
        {
            callback.ChainAppend(" ")
                    .ChainAppendTypeName(typeSuffix);
        }
        callback.AppendLine("[]");
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

    private static bool TypeIsGenericSequence(Type instanceType)
         => instanceType.IsGenericType && new[]
         {
            "Enumerable",
            "Collection",
            "List",
            "Array"
         }.Any(x => instanceType.GetGenericTypeDefinition().FullName.Contains(x));


    private static bool IsGenericCollectionOrDerrivedType(CustomTypeHandlerRequest request)
        => IsGenericCollection(request.InstanceType)
            || IsGenericReadOnlyCollection(request.InstanceType)
            || IsGenericList(request.InstanceType);

    private static bool IsGenericCollection(Type? t)
        => t != null && t.IsGenericType
            && t.GetGenericTypeDefinition() == typeof(Collection<>);

    private static bool IsGenericReadOnlyCollection(Type? t)
        => t != null && t.IsGenericType
            && t.GetGenericTypeDefinition() == typeof(ReadOnlyCollection<>);

    private static bool IsGenericList(Type? t)
        => t != null && t.IsGenericType
            && t.GetGenericTypeDefinition() == typeof(List<>);
}
