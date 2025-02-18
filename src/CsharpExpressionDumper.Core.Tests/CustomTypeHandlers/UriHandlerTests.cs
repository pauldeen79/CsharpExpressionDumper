namespace CsharpExpressionDumper.Core.Tests.CustomTypeHandlers;

public class UriHandlerTests
{
    [Fact]
    public void Can_Process_Uri()
    {
        // Arrange
        var sut = new UriHandler();
        var instance = new Uri("http://www.google.com/");
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = Enumerable.Empty<ICustomTypeHandler>();
        var typeNameFormatters = new[] { new DefaultTypeNameFormatter() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.ShouldBeTrue();
        code.ShouldBe(@"new System.Uri(""http://www.google.com/"")");
    }

    [Fact]
    public void Can_Process_Uri_With_Abbreviated_TypeNames()
    {
        // Arrange
        var sut = new UriHandler();
        var instance = new Uri("http://www.google.com/");
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = Enumerable.Empty<ICustomTypeHandler>();
        var typeNameFormatters = new[] { new SkipNamespacesTypeNameFormatter(new[] { "System" }) };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.ShouldBeTrue();
        code.ShouldBe(@"new Uri(""http://www.google.com/"")");
    }
}
