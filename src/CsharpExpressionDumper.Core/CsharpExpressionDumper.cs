using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Commands;
using CsharpExpressionDumper.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsharpExpressionDumper.Core
{
    public class CsharpExpressionDumper
    {
        private readonly IReadOnlyCollection<IObjectHandler> _objectHandlers;
        private readonly IReadOnlyCollection<ICustomTypeHandler> _customTypeHandlers;
        private readonly ICsharpExpressionDumperCallback _instanceCallback;

        public CsharpExpressionDumper(IEnumerable<IObjectHandler> objectHandlers,
                                      IEnumerable<ICustomTypeHandler> customTypeHandlers,
                                      ICsharpExpressionDumperCallback instanceCallback)
        {
            _objectHandlers = new List<IObjectHandler>(objectHandlers ?? Enumerable.Empty<IObjectHandler>());
            _customTypeHandlers = new List<ICustomTypeHandler>(customTypeHandlers ?? Enumerable.Empty<ICustomTypeHandler>());
            _instanceCallback = instanceCallback ?? throw new ArgumentNullException(nameof(instanceCallback));
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
            _instanceCallback.ProcessRecursiveCallbackDelegate = DoProcessRecursive;
            _instanceCallback.Builder = builder;
            var instanceCommand = new CustomTypeHandlerCommand(instance, instanceType, level);
            var instanceIsCustom = _customTypeHandlers.ProcessUntilSuccess(x => x.Process(instanceCommand, _instanceCallback));
            if (!instanceIsCustom)
            {
                var isAnonymousType = instanceType.IsAnonymousType();
                _instanceCallback.Append("new ");

                if (!isAnonymousType)
                {
                    _instanceCallback.AppendTypeName(instanceType);
                }

                var objectHandlerCommand = new ObjectHandlerCommand(instance, instanceType, level, type, isAnonymousType);
                var success = _objectHandlers.ProcessUntilSuccess(x => x.ProcessInstance(objectHandlerCommand, _instanceCallback));
                if (!success)
                {
                    throw new InvalidOperationException($"There is no object handler which supports object of type [{instanceType?.FullName}]");
                }
            }
        }
    }
}
