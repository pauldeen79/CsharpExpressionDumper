namespace CsharpExpressionDumper.Tests.TestData
{
    public class MyClassWithOnlyReadOnlyProperties
    {
        public string Property1 { get; }
        public int Property2 { get; }

        public MyClassWithOnlyReadOnlyProperties()
        {
            Property1 = "Test";
            Property2 = 13;
        }
    }
}
