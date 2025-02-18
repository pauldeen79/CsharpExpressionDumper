namespace CsharpExpressionDumper.Core.Tests.CustomTypeHandlers;

public class GuidHandlerTests
{
    [Fact]
    public void Can_Process_Guid()
    {
        // Arrange
        var sut = new GuidHandler();
        var instance = Guid.Empty;
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = Enumerable.Empty<ICustomTypeHandler>();
        var typeNameFormatters = new[] { new DefaultTypeNameFormatter() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.ShouldBeTrue();
        code.ShouldBe(@"new System.Guid(""00000000-0000-0000-0000-000000000000"")");
    }

    [Fact]
    public void Can_Process_Guid_With_Abbreviated_TypeNames()
    {
        // Arrange
        var sut = new GuidHandler();
        var instance = Guid.Empty;
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = Enumerable.Empty<ICustomTypeHandler>();
        var typeNameFormatters = new[] { new SkipNamespacesTypeNameFormatter(new[] { "System" }) };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.ShouldBeTrue();
        code.ShouldBe(@"new Guid(""00000000-0000-0000-0000-000000000000"")");
    }
}
