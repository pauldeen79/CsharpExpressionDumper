﻿namespace CsharpExpressionDumper.Core.Tests.TestData;

public class MyClassWithFlatClassReadOnlyCollection
{
    public string? Property1 { get; set; }
    public ReadOnlyCollection<MyFlatClass>? Property2 { get; set; }
}
