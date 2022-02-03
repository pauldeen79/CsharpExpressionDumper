namespace CsharpExpressionDumper.Core.Tests.CustomTypeHandlers;

public class TimeSpanHandlerTests
{
    [Fact]
    public void Can_Process_TimeSpan()
    {
        // Arrange
        var sut = new TimeSpanHandler();
        var instance = new TimeSpan(1, 2, 3, 4, 5);
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = Enumerable.Empty<ICustomTypeHandler>();
        var typeNameFormatters = new[] { new DefaultTypeNameFormatter() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new System.TimeSpan(1, 2, 3, 4, 5)");
    }

    [Fact]
    public void Can_Process_TimeSpan_With_Abbreviated_TypeNames()
    {
        // Arrange
        var sut = new TimeSpanHandler();
        var instance = new TimeSpan(1, 2, 3, 4, 5);
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = Enumerable.Empty<ICustomTypeHandler>();
        var typeNameFormatters = new[] { new SkipNamespacesTypeNameFormatter(new[] { "System" }) };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new TimeSpan(1, 2, 3, 4, 5)");
    }
}
