# CsharpExpressionDumper
Generates c# initialization code from object instances

Example:
var input = new { Property1 = "test", Property2 = 2 };

var dumper = new CsharpExpressionDumper
(
    Default.ObjectHandlers,
    customTypeHandlers.Concat(Default.CustomTypeHandlers),
    Default.TypeNameFormatters,
    Default.ConstructorResolvers,
    Default.ReadOnlyPropertyResolvers
);

var sourceCode = dumper.Dump(input, typeof(T));
