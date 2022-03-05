namespace CsharpExpressionDumper.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCsharpExpressionDumper(this IServiceCollection instance)
        => instance.AddCsharpExpressionDumper(_ => { });
    
    public static IServiceCollection AddCsharpExpressionDumper(this IServiceCollection instance,
                                                               Action<IServiceCollection> customConfigurationAction)
    {
        instance.TryAddTransient<ICsharpExpressionDumper, CsharpExpressionDumper>();
        customConfigurationAction.Invoke(instance);
        if (!instance.Any(x => x.ImplementationType == typeof(DefaultCsharpExpressionDumperCallback)))
        {
            instance.TryAddTransient<ICsharpExpressionDumperCallback, DefaultCsharpExpressionDumperCallback>();
        }
        if (!instance.Any(x => x.ImplementationType == typeof(DefaultObjectHandler)))
        {
            instance.AddTransient<IObjectHandler, DefaultObjectHandler>();
        }
        instance.TryAddTransient<IConstructorResolver, DefaultConstructorResolver>();
        instance.TryAddTransient<IReadOnlyPropertyResolver, DefaultReadOnlyPropertyResolver>();
        instance.TryAddTransient<ITypeNameFormatter, DefaultTypeNameFormatter>();
        if (!instance.Any(x => x.ImplementationType == typeof(NullHandler)))
        {
            instance.AddTransient<ICustomTypeHandler, NullHandler>();
            instance.AddTransient<ICustomTypeHandler, DateTimeHandler>();
            instance.AddTransient<ICustomTypeHandler, TimeSpanHandler>();
            instance.AddTransient<ICustomTypeHandler, EnumHandler>();
            instance.AddTransient<ICustomTypeHandler, BooleanHandler>();
            instance.AddTransient<ICustomTypeHandler, CharHandler>();
            instance.AddTransient<ICustomTypeHandler, GuidHandler>();
            instance.AddTransient<ICustomTypeHandler, TypeHandler>();
            instance.AddTransient<ICustomTypeHandler, UriHandler>();
            instance.AddTransient<ICustomTypeHandler, VersionHandler>();
            instance.AddTransient<ICustomTypeHandler, KeyValuePairHandler>();
            instance.AddTransient<ICustomTypeHandler, ValueTupleHandler>();
            instance.AddTransient<ICustomTypeHandler, ValueTypeHandler>();
            instance.AddTransient<ICustomTypeHandler, StringHandler>();
            instance.AddTransient<ICustomTypeHandler, DictionaryHandler>();
            instance.AddTransient<ICustomTypeHandler, EnumerableHandler>();
        }
        return instance;
    }
}
