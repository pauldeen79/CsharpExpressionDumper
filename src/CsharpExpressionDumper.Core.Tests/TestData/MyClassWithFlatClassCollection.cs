﻿using System.Collections.ObjectModel;

namespace CsharpExpressionDumper.Core.Tests.TestData
{
    public class MyClassWithFlatClassCollection
    {
        public string? Property1 { get; set; }
        public Collection<MyFlatClass>? Property2 { get; set; }
    }
}
