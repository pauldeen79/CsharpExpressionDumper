namespace CsharpExpressionDumper.Core.Tests.CustomTypeHandlers;

public class DictionaryHandlerTests
{
    [Fact]
    public void Can_Process_Generic_Dictionary()
    {
        // Arrange
        var sut = new DictionaryHandler();
        var instance = new Dictionary<string, string> { { "k1", "a" }, { "k2", "b" }, { "k3", "c" } };
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = new[] { new StringHandler() };
        var typeNameFormatters = new[] { new DefaultTypeNameFormatter() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new System.Collections.Generic.Dictionary<System.String, System.String>
{
    [@""k1""] = @""a"",
    [@""k2""] = @""b"",
    [@""k3""] = @""c"",
}");
    }

    [Fact]
    public void Can_Process_Generic_Dictionary_With_Abbreviated_TypeNames()
    {
        // Arrange
        var sut = new DictionaryHandler();
        var instance = new Dictionary<string, string> { { "k1", "a" }, { "k2", "b" }, { "k3", "c" } };
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = new[] { new StringHandler() };
        var typeNameFormatters = new[] { new SkipNamespacesTypeNameFormatter(new[] { "System", "System.Collections.Generic" }) };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new Dictionary<String, String>
{
    [@""k1""] = @""a"",
    [@""k2""] = @""b"",
    [@""k3""] = @""c"",
}");
    }
}
