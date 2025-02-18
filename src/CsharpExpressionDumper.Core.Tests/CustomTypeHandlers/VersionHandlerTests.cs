namespace CsharpExpressionDumper.Core.Tests.CustomTypeHandlers;

public class VersionHandlerTests
{
    [Fact]
    public void Can_Process_Version()
    {
        // Arrange
        var sut = new VersionHandler();
        var instance = new Version(1, 2, 3, 4);
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = Enumerable.Empty<ICustomTypeHandler>();
        var typeNameFormatters = new[] { new DefaultTypeNameFormatter() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.ShouldBeTrue();
        code.ShouldBe(@"new System.Version(1, 2, 3, 4)");
    }

    [Fact]
    public void Can_Process_Version_With_Abbreviated_TypeNames()
    {
        // Arrange
        var sut = new VersionHandler();
        var instance = new Version(1, 2, 3, 4);
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = Enumerable.Empty<ICustomTypeHandler>();
        var typeNameFormatters = new[] { new SkipNamespacesTypeNameFormatter(new[] { "System" }) };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.ShouldBeTrue();
        code.ShouldBe(@"new Version(1, 2, 3, 4)");
    }
}
