using System.Collections.Generic;

namespace CsharpExpressionDumper.Core.Tests.TestData
{
    public class MyClassWithFlatClassIList
    {
        public string? Property1 { get; set; }
        public IList<MyFlatClass>? Property2 { get; set; }
    }
}
