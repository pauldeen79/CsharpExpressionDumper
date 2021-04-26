using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.Abstractions.Requests;
using CsharpExpressionDumper.Core.Extensions;

namespace CsharpExpressionDumper.Core.Tests.TestData
{
    public class MyCustomTypeHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
        {
            if (request.Instance is MyImmutableClass imm)
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
