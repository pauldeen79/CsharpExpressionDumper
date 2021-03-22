# CsharpExpressionDumper
Generates c# initialization code from object instances

Example:
var input = new { Property1 = "test", Property2 = 2 };

var actual = Dump(input);

