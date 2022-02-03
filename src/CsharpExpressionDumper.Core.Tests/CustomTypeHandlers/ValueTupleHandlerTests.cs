namespace CsharpExpressionDumper.Core.Tests.CustomTypeHandlers;

public class ValueTupleHandlerTests
{
    [Fact]
    public void Can_Process_ValueTuple()
    {
        // Arrange
        var sut = new ValueTupleHandler();
        var instance = new ValueTuple<string, int>("test", 19);
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = new ICustomTypeHandler[] { new StringHandler(), new ValueTypeHandler() };
        var typeNameFormatters = new[] { new DefaultTypeNameFormatter() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new System.ValueTuple<System.String, System.Int32>(@""test"", 19)");
    }

    [Fact]
    public void Can_Process_ValueTuple_With_Abbreviated_TypeNames()
    {
        // Arrange
        var sut = new ValueTupleHandler();
        var instance = new ValueTuple<string, int>("test", 19);
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = new ICustomTypeHandler[] { new StringHandler(), new ValueTypeHandler() };
        var typeNameFormatters = new[] { new SkipNamespacesTypeNameFormatter(new[] { "System" }) };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new ValueTuple<String, Int32>(@""test"", 19)");
    }
}
