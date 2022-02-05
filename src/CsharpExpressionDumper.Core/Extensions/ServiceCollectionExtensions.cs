namespace CsharpExpressionDumper.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCsharpExpressionDumper(this IServiceCollection instance)
        => instance.AddCsharpExpressionDumper(_ => { });
    
    public static IServiceCollection AddCsharpExpressionDumper(this IServiceCollection instance,
                                                               Action<IServiceCollection> customConfigurationAction)
    {
        instance.AddTransient<ICsharpExpressionDumper, CsharpExpressionDumper>();
        instance.AddTransient<ICsharpExpressionDumperCallback, DefaultCsharpExpressionDumperCallback>();
        instance.AddTransient<IObjectHandler, DefaultObjectHandler>();
        instance.AddTransient<IConstructorResolver, DefaultConstructorResolver>();
        instance.AddTransient<IReadOnlyPropertyResolver, DefaultReadOnlyPropertyResolver>();
        customConfigurationAction.Invoke(instance);
        instance.AddTransient<ITypeNameFormatter, DefaultTypeNameFormatter>();
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
        return instance;
    }
}
