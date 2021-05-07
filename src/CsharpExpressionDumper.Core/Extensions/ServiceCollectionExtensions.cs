using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Core.ConstructorResolvers;
using CsharpExpressionDumper.Core.CsharpExpressionDumperCallbacks;
using CsharpExpressionDumper.Core.CustomTypeHandlers;
using CsharpExpressionDumper.Core.ObjectHandlers;
using CsharpExpressionDumper.Core.ReadOnlyPropertyResolvers;
using CsharpExpressionDumper.Core.TypeNameFormatters;
using Microsoft.Extensions.DependencyInjection;

namespace CsharpExpressionDumper.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCsharpExpressionDumper(this IServiceCollection instance)
        {
            instance.AddTransient<ICsharpExpressionDumper, CsharpExpressionDumper>();
            instance.AddTransient<ICsharpExpressionDumperCallback, DefaultCsharpExpressionDumperCallback>();
            instance.AddSingleton<IObjectHandler, DefaultObjectHandler>();
            instance.AddSingleton<IConstructorResolver, DefaultConstructorResolver>();
            instance.AddSingleton<IReadOnlyPropertyResolver, DefaultReadOnlyPropertyResolver>();
            instance.AddSingleton<ITypeNameFormatter, DefaultTypeNameFormatter>();
            instance.AddSingleton<ICustomTypeHandler, NullHandler>();
            instance.AddSingleton<ICustomTypeHandler, DateTimeHandler>();
            instance.AddSingleton<ICustomTypeHandler, TimeSpanHandler>();
            instance.AddSingleton<ICustomTypeHandler, EnumHandler>();
            instance.AddSingleton<ICustomTypeHandler, BooleanHandler>();
            instance.AddSingleton<ICustomTypeHandler, CharHandler>();
            instance.AddSingleton<ICustomTypeHandler, GuidHandler>();
            instance.AddSingleton<ICustomTypeHandler, TypeHandler>();
            instance.AddSingleton<ICustomTypeHandler, UriHandler>();
            instance.AddSingleton<ICustomTypeHandler, VersionHandler>();
            instance.AddSingleton<ICustomTypeHandler, KeyValuePairHandler>();
            instance.AddSingleton<ICustomTypeHandler, ValueTupleHandler>();
            instance.AddSingleton<ICustomTypeHandler, ValueTypeHandler>();
            instance.AddSingleton<ICustomTypeHandler, StringHandler>();
            instance.AddSingleton<ICustomTypeHandler, DictionaryHandler>();
            instance.AddSingleton<ICustomTypeHandler, EnumerableHandler>();
            return instance;
        }
    }
}
