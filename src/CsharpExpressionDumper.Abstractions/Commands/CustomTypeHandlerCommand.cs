using System;

namespace CsharpExpressionDumper.Abstractions.Commands
{
    public class CustomTypeHandlerCommand
    {
        public object Instance { get; }
        public Type InstanceType { get; }
        public int Level { get; }

        public CustomTypeHandlerCommand(object instance,
                                        Type instanceType,
                                        int level)
        {
            Instance = instance;
            InstanceType = instanceType;
            Level = level;
        }
    }
}