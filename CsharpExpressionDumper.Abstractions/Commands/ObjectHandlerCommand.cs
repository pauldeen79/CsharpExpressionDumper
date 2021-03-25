using System;

namespace CsharpExpressionDumper.Abstractions.Commands
{
    public class ObjectHandlerCommand
    {
        public object Instance { get; }
        public Type InstanceType { get; }
        public int Level { get; }
        public Type Type { get; }
        public bool IsAnonymousType { get; }

        public ObjectHandlerCommand(object instance,
                                    Type instanceType,
                                    int level,
                                    Type type,
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