﻿using System.Collections.Generic;

namespace CsharpExpressionDumper.Tests.TestData
{
    public class MyClassWithFlatClassICollection
    {
        public string Property1 { get; set; }
        public ICollection<MyFlatClass> Property2 { get; set; }
    }
}
