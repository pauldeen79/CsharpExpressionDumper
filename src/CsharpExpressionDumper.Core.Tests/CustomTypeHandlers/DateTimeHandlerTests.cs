namespace CsharpExpressionDumper.Core.Tests.CustomTypeHandlers;

public class DateTimeHandlerTests
{
    [Fact]
    public void Can_Process_DateTime()
    {
        // Arrange
        var sut = new DateTimeHandler();
        var instance = new DateTime(1900, 1, 2, 3, 4, 5, 0, DateTimeKind.Unspecified);
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = Enumerable.Empty<ICustomTypeHandler>();
        var typeNameFormatters = new[] { new DefaultTypeNameFormatter() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new System.DateTime(1900, 1, 2, 3, 4, 5, 0, System.DateTimeKind.Unspecified)");
    }

    [Fact]
    public void Can_Process_DateTime_With_Abbreviated_TypeNames()
    {
        // Arrange
        var sut = new DateTimeHandler();
        var instance = new DateTime(1900, 1, 2, 3, 4, 5, 0, DateTimeKind.Unspecified);
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = Enumerable.Empty<ICustomTypeHandler>();
        var typeNameFormatters = new[] { new SkipNamespacesTypeNameFormatter(new[] { "System" }) };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new DateTime(1900, 1, 2, 3, 4, 5, 0, DateTimeKind.Unspecified)");
    }
}
