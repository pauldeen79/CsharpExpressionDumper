namespace CsharpExpressionDumper.Core.Tests.CustomTypeHandlers;

public class KeyValuePairHandlerTests
{
    [Fact]
    public void Can_Process_Generic_KeyValuePair()
    {
        // Arrange
        var sut = new KeyValuePairHandler();
        var instance = new KeyValuePair<string, string>("Key", "Value");
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = new[] { new StringHandler() };
        var typeNameFormatters = new[] { new DefaultTypeNameFormatter() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new System.Collections.Generic.KeyValuePair<System.String, System.String>(@""Key"", @""Value"")");
    }

    [Fact]
    public void Can_Process_Generic_KeyValuePair_With_Abbreviated_TypeNames()
    {
        // Arrange
        var sut = new KeyValuePairHandler();
        var instance = new KeyValuePair<string, string>("Key", "Value");
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = new[] { new StringHandler() };
        var typeNameFormatters = new[] { new SkipNamespacesTypeNameFormatter(new[] { "System", "System.Collections.Generic" }) };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new KeyValuePair<String, String>(@""Key"", @""Value"")");
    }
}
