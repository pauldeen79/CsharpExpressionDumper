﻿namespace CsharpExpressionDumper.Core.Tests.TestFixtures;

internal static class TestHelpers
{
    internal static CustomTypeHandlerRequest CreateCustomTypeHandlerRequest(object? instance)
        => new CustomTypeHandlerRequest(instance, instance?.GetType(), 0);

    internal static DefaultCsharpExpressionDumperCallback CreateCallback(IEnumerable<ICustomTypeHandler> typeHandlers,
                                                                         IEnumerable<ITypeNameFormatter> typeNameFormatters)
    {
        var callback = new DefaultCsharpExpressionDumperCallback
        (
            typeHandlers,
            typeNameFormatters,
            Enumerable.Empty<IConstructorResolver>(),
            Enumerable.Empty<IReadOnlyPropertyResolver>(),
            Enumerable.Empty<IObjectHandlerPropertyFilter>()
        );
        //little hacky... this initializes the ProcessRecursiveCallbackDelegate property on the callback class
        callback.Initialize
        (
            new Action<object?, Type?, StringBuilder, int>((instance, type, builder, level) => typeHandlers.ProcessUntilSuccess(x => x.Process(new CustomTypeHandlerRequest(instance, type, level), callback))),
            new StringBuilder()
        );
        return callback;
    }
}
