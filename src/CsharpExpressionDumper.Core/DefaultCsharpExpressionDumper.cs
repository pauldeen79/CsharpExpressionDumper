namespace CsharpExpressionDumper.Core;

public class DefaultCsharpExpressionDumper : ICsharpExpressionDumper
{
    private readonly IReadOnlyCollection<IObjectHandler> _objectHandlers;
    private readonly IReadOnlyCollection<ICustomTypeHandler> _customTypeHandlers;
    private readonly ICsharpExpressionDumperCallback _instanceCallback;

    public DefaultCsharpExpressionDumper(IEnumerable<IObjectHandler> objectHandlers,
                                  IEnumerable<ICustomTypeHandler> customTypeHandlers,
                                  ICsharpExpressionDumperCallback instanceCallback)
    {
        _objectHandlers = new List<IObjectHandler>(objectHandlers);
        _customTypeHandlers = new List<ICustomTypeHandler>(customTypeHandlers);
        _instanceCallback = instanceCallback;
    }

    public string Dump(object? instance, Type? type = null)
    {
        var builder = new StringBuilder();
        _instanceCallback.Initialize(DoProcessRecursive, builder);
        DoProcessRecursive(instance, type, builder, 0);
        return builder.ToString();
    }

    private void DoProcessRecursive(object? instance, Type? type, StringBuilder builder, int level)
    {
        var instanceType = type ?? instance?.GetType();
        var instanceRequest = new CustomTypeHandlerRequest(instance, instanceType, level);
        var instanceIsCustom = _customTypeHandlers.ProcessUntilSuccess(x => x.Process(instanceRequest, _instanceCallback));
        if (!instanceIsCustom && instanceType != null)
        {
            var isAnonymousType = instanceType.IsAnonymousType();
            _instanceCallback.Append("new ");

            if (!isAnonymousType)
            {
                _instanceCallback.AppendTypeName(instanceType);
            }

            var objectHandlerCommand = new ObjectHandlerRequest(instance, instanceType, level, type, isAnonymousType);
            var success = _objectHandlers.ProcessUntilSuccess(x => x.ProcessInstance(objectHandlerCommand, _instanceCallback));
            if (!success)
            {
                throw new InvalidOperationException($"There is no object handler which supports object of type [{instanceType?.FullName}]");
            }
        }
    }
}
