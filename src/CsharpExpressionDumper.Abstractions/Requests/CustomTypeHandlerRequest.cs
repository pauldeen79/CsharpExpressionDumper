using System;

namespace CsharpExpressionDumper.Abstractions.Requests
{
    public class CustomTypeHandlerRequest
    {
        public object? Instance { get; }
        public Type? InstanceType { get; }
        public int Level { get; }

        public CustomTypeHandlerRequest(object? instance,
                                        Type? instanceType,
                                        int level)
        {
            Instance = instance;
            InstanceType = instanceType;
            Level = level;
        }
    }
}
