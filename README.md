# CsharpExpressionDumper
Generates c# initialization code from object instances

Example:
```C#
var input = new { Property1 = "test", Property2 = 2 };

var dumper = new CsharpExpressionDumper
(
    Default.ObjectHandlers,
    Default.CustomTypeHandlers,
    new DefaultCsharpExpressionDumperCallback
    (
        Default.CustomTypeHandlers,
        Default.TypeNameFormatters,
        Default.ConstructorResolvers,
        Default.ReadOnlyPropertyResolvers,
        Default.ObjectHandlerPropertyFilters
    )
);

var sourceCode = dumper.Dump(input, input.GetType());
// generates: new { Property1 = "test", Property2 = 2 }
```

See unit tests for more examples.
