using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Extensions;

namespace CsharpExpressionDumper.CsharpExpressionDumperCallbacks
{
    public class DefaultCsharpExpressionDumperCallback : ICsharpExpressionDumperCallback
    {
        private readonly Action<object, Type, StringBuilder, int> _processRecursiveCallbackDelegate;
        private readonly StringBuilder _builder;
        private string Prefix { get; set; }
        private string Suffix { get; set; }
        private readonly IEnumerable<ICustomTypeHandler> _typeHandlers;
        private readonly IEnumerable<ITypeNameFormatter> _typeNameFormatters;
        private readonly IEnumerable<IConstructorResolver> _constructorResolvers;
        private readonly IEnumerable<IReadOnlyPropertyResolver> _readOnlyPropertyResolvers;
        private readonly IEnumerable<IObjectHandlerPropertyFilter> _objectHandlerPropertyFilters;

        public DefaultCsharpExpressionDumperCallback(Action<object, Type, StringBuilder, int> processRecursiveCallbackDelegate,
                                                     StringBuilder builder,
                                                     IEnumerable<ICustomTypeHandler> typeHandlers,
                                                     IEnumerable<ITypeNameFormatter> typeNameFormatters,
                                                     IEnumerable<IConstructorResolver> constructorResolvers,
                                                     IEnumerable<IReadOnlyPropertyResolver> readOnlyPropertyResolvers,
                                                     IEnumerable<IObjectHandlerPropertyFilter> objectHandlerPropertyFilters)
        {
            _processRecursiveCallbackDelegate = processRecursiveCallbackDelegate;
            _builder = builder;
            _typeHandlers = typeHandlers;
            _typeNameFormatters = typeNameFormatters;
            _constructorResolvers = constructorResolvers;
            _readOnlyPropertyResolvers = readOnlyPropertyResolvers;
            _objectHandlerPropertyFilters = objectHandlerPropertyFilters;
        }

        public void Append(object value)
            => _builder.Append(value);

        public void AppendFormattedString(string value)
            => ProcessRecursive(value, typeof(string), 0);

        public void AppendLine(object value)
            => _builder.Append(value)
                       .AppendLine();

        public void AppendLine()
            => _builder.AppendLine();

        public void AppendPrefix()
            => Append(Prefix);

        public void AppendSingleValue(object value)
            => _builder.Append(Prefix)
                       .Append(value)
                       .Append(Suffix);

        public void AppendSuffix()
            => Append(Suffix);

        public void AppendTypeName(Type type)
        {
            var typeName = _typeNameFormatters.ProcessUntilSuccess(x => x.Format(type));

            if (typeName == null)
            {
                throw new ArgumentException($"Typename of type [{type.FullName}] could not be formatted");
            }

            Append(typeName);
        }

        private ICsharpExpressionDumperCallback CreateNestedCallback(string prefix, string suffix)
            => new DefaultCsharpExpressionDumperCallback
            (
                _processRecursiveCallbackDelegate,
                _builder,
                _typeHandlers,
                _typeNameFormatters,
                _constructorResolvers,
                _readOnlyPropertyResolvers,
                _objectHandlerPropertyFilters
            )
            { Prefix = prefix, Suffix = suffix };

        public void ProcessRecursive(object instance, Type type, int level)
            => _processRecursiveCallbackDelegate.Invoke
               (
                   instance,
                   type,
                   _builder,
                   level
               );

        public bool IsPropertyCustom(CustomTypeHandlerCommand propertyCommand, string prefix, string suffix)
        {
            var propertyCallback = CreateNestedCallback(prefix, suffix);
            return _typeHandlers.ProcessUntilSuccess(x => x.Process(propertyCommand, propertyCallback));
        }

        public bool IsPropertyValid(ObjectHandlerCommand command, PropertyInfo propertyInfo)
            => _objectHandlerPropertyFilters.All(x => x.IsValid(command, propertyInfo));

        public ConstructorInfo ResolveConstructor(Type type)
            => _constructorResolvers.ProcessUntilSuccess(x => x.Resolve(type));

        public PropertyInfo ResolveReadOnlyProperty(PropertyInfo[] properties, ConstructorInfo ctor, ParameterInfo argument)
            => _readOnlyPropertyResolvers.ProcessUntilSuccess(x => x.Process(properties, ctor, argument));
    }
}
