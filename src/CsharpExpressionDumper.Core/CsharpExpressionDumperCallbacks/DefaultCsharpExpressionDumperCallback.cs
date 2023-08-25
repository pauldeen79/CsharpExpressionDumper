namespace CsharpExpressionDumper.Core.CsharpExpressionDumperCallbacks;

internal class DefaultCsharpExpressionDumperCallback : ICsharpExpressionDumperCallback
{
    public Action<object?, Type?, StringBuilder, int> ProcessRecursiveCallbackDelegate { get; private set; }
        = new Action<object?, Type?, StringBuilder, int>((_1, _2, _3, _4) => { });
    public StringBuilder Builder { get; private set; } = new StringBuilder();
    private string Prefix { get; set; } = "";
    private string Suffix { get; set; } = "";
    private readonly IEnumerable<ICustomTypeHandler> _typeHandlers;
    private readonly IEnumerable<ITypeNameFormatter> _typeNameFormatters;
    private readonly IEnumerable<IConstructorResolver> _constructorResolvers;
    private readonly IEnumerable<IReadOnlyPropertyResolver> _readOnlyPropertyResolvers;
    private readonly IEnumerable<IObjectHandlerPropertyFilter> _objectHandlerPropertyFilters;

    public DefaultCsharpExpressionDumperCallback(IEnumerable<ICustomTypeHandler> typeHandlers,
                                                 IEnumerable<ITypeNameFormatter> typeNameFormatters,
                                                 IEnumerable<IConstructorResolver> constructorResolvers,
                                                 IEnumerable<IReadOnlyPropertyResolver> readOnlyPropertyResolvers,
                                                 IEnumerable<IObjectHandlerPropertyFilter> objectHandlerPropertyFilters)
    {
        _typeHandlers = typeHandlers;
        _typeNameFormatters = typeNameFormatters;
        _constructorResolvers = constructorResolvers;
        _readOnlyPropertyResolvers = readOnlyPropertyResolvers;
        _objectHandlerPropertyFilters = objectHandlerPropertyFilters;
    }

    public void Append(object value)
        => Builder.Append(value);

    public void AppendFormattedString(string value)
        => ProcessRecursive(value, typeof(string), 0);

    public void AppendLine(object value)
        => Builder.Append(value)
                  .AppendLine();

    public void AppendLine()
        => Builder.AppendLine();

    public void AppendPrefix()
        => Append(Prefix);

    public void AppendSingleValue(object value)
        => Builder.Append(Prefix)
                  .Append(value)
                  .Append(Suffix);

    public void AppendSuffix()
        => Append(Suffix);

    public void AppendTypeName(Type type)
        => Append(_typeNameFormatters.Aggregate(type.FullName.FixTypeName(), (seed, func) => func.Format(seed) ?? seed));

    private ICsharpExpressionDumperCallback CreateNestedCallback(string prefix, string suffix)
        => new DefaultCsharpExpressionDumperCallback
        (
            _typeHandlers,
            _typeNameFormatters,
            _constructorResolvers,
            _readOnlyPropertyResolvers,
            _objectHandlerPropertyFilters
        )
        {
            Prefix = prefix,
            Suffix = suffix,
            ProcessRecursiveCallbackDelegate = ProcessRecursiveCallbackDelegate,
            Builder = Builder
        };

    public void ProcessRecursive(object? instance, Type? type, int level)
        => ProcessRecursiveCallbackDelegate.Invoke(instance,
                                                   type,
                                                   Builder,
                                                   level);

    public bool IsPropertyCustom(CustomTypeHandlerRequest propertyCommand, string prefix, string suffix)
        => _typeHandlers.ProcessUntilSuccess(x => x.Process(propertyCommand, CreateNestedCallback(prefix, suffix)));

    public bool IsPropertyValid(ObjectHandlerRequest command, PropertyInfo propertyInfo)
        => _objectHandlerPropertyFilters.All(x => x.IsValid(command, propertyInfo));

    public ConstructorInfo? ResolveConstructor(Type type)
        => _constructorResolvers.ProcessUntilSuccess(x => x.Resolve(type));

    public PropertyInfo? ResolveReadOnlyProperty(PropertyInfo[] properties, ConstructorInfo ctor, ParameterInfo argument)
        => _readOnlyPropertyResolvers.ProcessUntilSuccess(x => x.Process(properties, ctor, argument));

    public void Initialize(Action<object?, Type?, StringBuilder, int> processRecursiveCallbackDelegate, StringBuilder builder)
    {
        ProcessRecursiveCallbackDelegate = processRecursiveCallbackDelegate;
        Builder = builder;
    }
}
