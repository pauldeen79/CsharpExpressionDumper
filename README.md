# CsharpExpressionDumper
Generates c# initialization code from object instances

Example:
```C#
//using Microsoft.Extensions.DependencyInjection;

var input = new { Property1 = "test", Property2 = 2 };

var serviceCollection = new ServiceCollection();
var serviceProvider = serviceCollection.AddCsharpExpressionDumper().BuildServiceProvider();
var dumper = serviceProvider.GetRequiredService<ICsharpExpressionDumper>();

var sourceCode = dumper.Dump(input, input.GetType());
// generates: new { Property1 = "test", Property2 = 2 }
```

See unit tests for more examples.
