﻿namespace CsharpExpressionDumper.Core.Tests.TestData;

public class MyClassWithFlatClassIReadOnlyCollection
{
    public string? Property1 { get; set; }
    public IReadOnlyCollection<MyFlatClass>? Property2 { get; set; }
}
