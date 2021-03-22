using Moq;
using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.Tests.TestFixtures
{
    public static class CallbackMock
    {
        public static Mock<ICsharpExpressionDumperCallback> Create()
        {
            return new Mock<ICsharpExpressionDumperCallback>();
        }
    }
}
