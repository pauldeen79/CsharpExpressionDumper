using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.CsharpExpressionDumperCallbacks;
using CsharpExpressionDumper.Extensions;

namespace CsharpExpressionDumper
{
    public class CsharpExpressionDumper
    {
        private readonly IReadOnlyCollection<IObjectHandler> _objectHandlers;
        private readonly IReadOnlyCollection<ICustomTypeHandler> _customTypeHandlers;
        private readonly IReadOnlyCollection<ITypeNameFormatter> _typeNameFormatters;
        private readonly IReadOnlyCollection<IConstructorResolver> _constructorResolvers;
        private readonly IReadOnlyCollection<IReadOnlyPropertyResolver> _readOnlyPropertyResolvers;

        public CsharpExpressionDumper(IEnumerable<IObjectHandler> objectHandlers,
                                      IEnumerable<ICustomTypeHandler> customTypeHandlers,
                                      IEnumerable<ITypeNameFormatter> typeNameFormatters,
                                      IEnumerable<IConstructorResolver> constructorResolvers,
                                      IEnumerable<IReadOnlyPropertyResolver> readOnlyPropertyResolvers)
        {
            _objectHandlers = new List<IObjectHandler>(objectHandlers ?? Enumerable.Empty<IObjectHandler>());
            _customTypeHandlers = new List<ICustomTypeHandler>(customTypeHandlers ?? Enumerable.Empty<ICustomTypeHandler>());
            _typeNameFormatters = new List<ITypeNameFormatter>(typeNameFormatters ?? Enumerable.Empty<ITypeNameFormatter>());
            _constructorResolvers = new List<IConstructorResolver>(constructorResolvers ?? Enumerable.Empty<IConstructorResolver>());
            _readOnlyPropertyResolvers = new List<IReadOnlyPropertyResolver>(readOnlyPropertyResolvers ?? Enumerable.Empty<IReadOnlyPropertyResolver>());
        }

        public string Dump(object instance, Type type = null)
        {
            var builder = new StringBuilder();
            DoProcessRecursive(instance, type, builder, 0);
            return builder.ToString();
        }

        private void DoProcessRecursive(object instance, Type type, StringBuilder builder, int level)
        {
            var instanceType = type ?? instance?.GetType();
            var instanceCallback = new DefaultCsharpExpressionDumperCallback
            (
                DoProcessRecursive,
                builder,
                string.Empty,
                string.Empty,
                _customTypeHandlers,
                _typeNameFormatters,
                _constructorResolvers,
                _readOnlyPropertyResolvers
            );
            var instanceCommand = new CustomTypeHandlerCommand(instance, instanceType, level);
            var instanceIsCustom = _customTypeHandlers.ProcessUntilSuccess(x => x.Process(instanceCommand, instanceCallback));
            if (!instanceIsCustom)
            {
                var isAnonymousType = instanceType.IsAnonymousType();
                instanceCallback.Append("new ");

                if (!isAnonymousType)
                {
                    instanceCallback.AppendTypeName(instanceType);
                }

                var objectHandlerCommand = new ObjectHandlerCommand(instance, instanceType, level, type, isAnonymousType);
                var success = _objectHandlers.ProcessUntilSuccess(x => x.ProcessInstance(objectHandlerCommand, instanceCallback));
                if (!success)
                {
                    throw new InvalidOperationException($"There is no object handler which supports object of type [{instanceType?.FullName}]");
                }
            }
        }
    }
}
