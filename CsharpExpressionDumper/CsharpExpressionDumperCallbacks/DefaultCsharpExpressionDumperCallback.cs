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
        private readonly string _prefix;
        private readonly string _suffix;
        private readonly IEnumerable<ICustomTypeHandler> _typeHandlers;
        private readonly IEnumerable<ITypeNameFormatter> _typeNameFormatters;
        private readonly IEnumerable<IConstructorResolver> _constructorResolvers;
        private readonly IEnumerable<IReadOnlyPropertyResolver> _readOnlyPropertyResolvers;
        private readonly IEnumerable<IObjectHandlerPropertyFilter> _objectHandlerPropertyFilters;

        public DefaultCsharpExpressionDumperCallback(Action<object, Type, StringBuilder, int> processRecursiveCallbackDelegate,
                                                     StringBuilder builder,
                                                     string prefix,
                                                     string suffix,
                                                     IEnumerable<ICustomTypeHandler> typeHandlers,
                                                     IEnumerable<ITypeNameFormatter> typeNameFormatters,
                                                     IEnumerable<IConstructorResolver> constructorResolvers,
                                                     IEnumerable<IReadOnlyPropertyResolver> readOnlyPropertyResolvers,
                                                     IEnumerable<IObjectHandlerPropertyFilter> objectHandlerPropertyFilters)
        {
            _processRecursiveCallbackDelegate = processRecursiveCallbackDelegate;
            _builder = builder;
            _prefix = prefix;
            _suffix = suffix;
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
            => Append(_prefix);

        public void AppendSingleValue(object value)
            => _builder.Append(_prefix)
                       .Append(value)
                       .Append(_suffix);

        public void AppendSuffix()
            => Append(_suffix);

        public void AppendTypeName(Type type)
        {
            var typeName = _typeNameFormatters.ProcessUntilSuccess(x => x.Format(type));

            if (typeName == null)
            {
                throw new ArgumentException($"Typename of type [{type.FullName}] could not be formatted");
            }

            Append(typeName);
        }

        public ICsharpExpressionDumperCallback CreateNestedCallback(string prefix, string suffix)
            => new DefaultCsharpExpressionDumperCallback
            (
                _processRecursiveCallbackDelegate,
                _builder,
                prefix,
                suffix,
                _typeHandlers,
                _typeNameFormatters,
                _constructorResolvers,
                _readOnlyPropertyResolvers,
                _objectHandlerPropertyFilters
            );

        public void ProcessRecursive(object instance, Type type, int level)
            => _processRecursiveCallbackDelegate.Invoke
               (
                   instance,
                   type,
                   _builder,
                   level
               );

        public bool IsPropertyCustom(CustomTypeHandlerCommand propertyCommand, ICsharpExpressionDumperCallback propertyCallback)
            => _typeHandlers.ProcessUntilSuccess(x => x.Process(propertyCommand, propertyCallback));

        public bool IsPropertyValid(ObjectHandlerCommand command, PropertyInfo propertyInfo)
            => _objectHandlerPropertyFilters.All(x => x.IsValid(command, propertyInfo));

        public ConstructorInfo ResolveConstructor(Type type)
            => _constructorResolvers.ProcessUntilSuccess(x => x.Resolve(type));

        public PropertyInfo ResolveReadOnlyProperty(PropertyInfo[] properties, ConstructorInfo ctor, ParameterInfo argument)
            => _readOnlyPropertyResolvers.ProcessUntilSuccess(x => x.Process(properties, ctor, argument));
    }
}
