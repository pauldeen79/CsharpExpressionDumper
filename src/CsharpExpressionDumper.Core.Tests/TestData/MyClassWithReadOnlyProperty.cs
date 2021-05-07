namespace CsharpExpressionDumper.Core.Tests.TestData
{
    public class MyClassWithReadOnlyProperty
    {
        public string? Property1 { get; set; }
        public int Property2 { get; }

        public MyClassWithReadOnlyProperty()
        {
            Property2 = 3;
        }
    }
}
