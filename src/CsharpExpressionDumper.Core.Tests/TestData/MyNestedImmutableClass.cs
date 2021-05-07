namespace CsharpExpressionDumper.Core.Tests.TestData
{
    public class MyNestedImmutableClass
    {
        public string Property1 { get; }
        public int Property2 { get; }
        public MyNestedImmutableClass? Property3 { get; }

        public MyNestedImmutableClass(string property1, int property2, MyNestedImmutableClass? property3)
        {
            Property1 = property1;
            Property2 = property2;
            Property3 = property3;
        }
    }
}
