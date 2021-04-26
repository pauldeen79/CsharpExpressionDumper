using System;

namespace CsharpExpressionDumper.Abstractions.Requests
{
    public class ObjectHandlerRequest
    {
        public object? Instance { get; }
        public Type? InstanceType { get; }
        public int Level { get; }
        public Type? Type { get; }
        public bool IsAnonymousType { get; }

        public ObjectHandlerRequest(object? instance,
                                    Type? instanceType,
                                    int level,
                                    Type? type,
                                    bool isAnonymousType)
        {
            Instance = instance;
            InstanceType = instanceType;
            Level = level;
            Type = type;
            IsAnonymousType = isAnonymousType;
        }
    }
}
