using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Extensions;

namespace CsharpExpressionDumper.Tests.TestData
{
    public class MyCustomTypeHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is MyImmutableClass imm)
            {
                callback.ChainAppendPrefix()
                        .ChainAppend("MyImmutableClass.Create(")
                        .ChainAppendFormattedString(imm.Property1)
                        .ChainAppend(", ")
                        .ChainAppend(imm.Property2)
                        .ChainAppend(')')
                        .ChainAppendSuffix();
                return true;
            }

            return false;
        }
    }
}
