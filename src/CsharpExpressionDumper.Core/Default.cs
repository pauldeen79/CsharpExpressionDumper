using System;
using System.Collections.Generic;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Core.ConstructorResolvers;
using CsharpExpressionDumper.Core.CustomTypeHandlers;
using CsharpExpressionDumper.Core.ObjectHandlers;
using CsharpExpressionDumper.Core.ReadOnlyPropertyResolvers;
using CsharpExpressionDumper.Core.TypeNameFormatters;

namespace CsharpExpressionDumper.Core
{
    public static class Default
    {
        public static IEnumerable<IObjectHandler> ObjectHandlers
            => _objectHandlers ?? (_objectHandlers = new IObjectHandler[]
                {
                    new DefaultObjectHandler(),
                });

        public static IEnumerable<ICustomTypeHandler> CustomTypeHandlers
            => _customTypeHandlers ?? (_customTypeHandlers = new ICustomTypeHandler[]
                {
                    new NullHandler(),
                    new DateTimeHandler(),
                    new TimeSpanHandler(),
                    new EnumHandler(),
                    new BooleanHandler(),
                    new CharHandler(),
                    new GuidHandler(),
                    new TypeHandler(),
                    new UriHandler(),
                    new VersionHandler(),
                    new KeyValuePairHandler(),
                    new ValueTupleHandler(),
                    new ValueTypeHandler(),
                    new StringHandler(),
                    new DictionaryHandler(),
                    new EnumerableHandler(),
                });

        public static IEnumerable<IConstructorResolver> ConstructorResolvers
            => _constructorResolvers ?? (_constructorResolvers = new IConstructorResolver[]
                {
                    new DefaultConstructorResolver(),
                });

        public static IEnumerable<IReadOnlyPropertyResolver> ReadOnlyPropertyResolvers
            => _readOnlyPropertyResolvers ?? (_readOnlyPropertyResolvers = new IReadOnlyPropertyResolver[]
                {
                    new DefaultReadOnlyPropertyResolver()
                });

        public static IEnumerable<ITypeNameFormatter> TypeNameFormatters
            => _typeNameFormatters ?? (_typeNameFormatters = new ITypeNameFormatter[]
                {
                    new DefaultTypeNameFormatter()
                });

        public static IEnumerable<IObjectHandlerPropertyFilter> ObjectHandlerPropertyFilters
            => _objectHandlerPropertyFilters ?? (_objectHandlerPropertyFilters = Array.Empty<IObjectHandlerPropertyFilter>());

        private static IEnumerable<IObjectHandler>? _objectHandlers;
        private static IEnumerable<ICustomTypeHandler>? _customTypeHandlers;
        private static IEnumerable<IConstructorResolver>? _constructorResolvers;
        private static IEnumerable<IReadOnlyPropertyResolver>? _readOnlyPropertyResolvers;
        private static IEnumerable<ITypeNameFormatter>? _typeNameFormatters;
        private static IEnumerable<IObjectHandlerPropertyFilter>? _objectHandlerPropertyFilters;
    }
}
