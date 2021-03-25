namespace CsharpExpressionDumper.Tests.TestData
{
    public class MyImmutableClassWithTwoCtors
    {
        public string Property1 { get; }
        public int Property2 { get; }

        public MyImmutableClassWithTwoCtors(string property1)
        {
            Property1 = property1;
        }

        public MyImmutableClassWithTwoCtors(string property1, int property2)
        {
            Property1 = property1;
            Property2 = property2;
        }
    }
}
