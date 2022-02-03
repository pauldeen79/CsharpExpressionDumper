namespace CsharpExpressionDumper.Core.Tests.CustomTypeHandlers;

public class EnumerableHandlerTests
{
    [Fact]
    public void Can_Process_Generic_List()
    {
        // Arrange
        var typeNameFormatters = new[] { new DefaultTypeNameFormatter() };
        var sut = new EnumerableHandler(typeNameFormatters);
        var instance = new List<string> { "a", "b", "c" };
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = new[] { new StringHandler() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new System.Collections.Generic.List<System.String>(new[]
{
    @""a"",
    @""b"",
    @""c"",
} )");
    }

    [Fact]
    public void Can_Process_Generic_List_With_Abbreviated_TypeNames()
    {
        // Arrange
        var typeNameFormatters = new[] { new SkipNamespacesTypeNameFormatter(new[] { "System", "System.Collections.Generic" }) };
        var sut = new EnumerableHandler(typeNameFormatters);
        var instance = new List<string> { "a", "b", "c" };
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = new[] { new StringHandler() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new List<String>(new[]
{
    @""a"",
    @""b"",
    @""c"",
} )");
    }

    [Fact]
    public void Can_Process_Array()
    {
        // Arrange
        var typeNameFormatters = new[] { new DefaultTypeNameFormatter() };
        var sut = new EnumerableHandler(typeNameFormatters);
        var instance = new string[] { "a", "b", "c" };
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = new[] { new StringHandler() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new[]
{
    @""a"",
    @""b"",
    @""c"",
}");
    }

    [Fact]
    public void Can_Process_Custom_GenericList_Type()
    {
        // Arrange
        var typeNameFormatters = new[] { new DefaultTypeNameFormatter() };
        var sut = new EnumerableHandler(typeNameFormatters);
        var instance = new CustomList<string> { "a", "b", "c" };
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = new[] { new StringHandler() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new CsharpExpressionDumper.Core.Tests.CustomTypeHandlers.EnumerableHandlerTests.CustomList<System.String>(new[]
{
    @""a"",
    @""b"",
    @""c"",
} )");
    }

    [Fact]
    public void Can_Process_Custom_GenericList_Type_With_Abbreviated_TypeNames()
    {
        // Arrange
        var typeNameFormatters = new[] { new SkipNamespacesTypeNameFormatter(new[] { "System", "CsharpExpressionDumper.Core.Tests.CustomTypeHandlers.EnumerableHandlerTests" }) };
        var sut = new EnumerableHandler(typeNameFormatters);
        var instance = new CustomList<string> { "a", "b", "c" };
        var request = TestHelpers.CreateCustomTypeHandlerRequest(instance);
        var typeHandlers = new[] { new StringHandler() };
        var callback = TestHelpers.CreateCallback(typeHandlers, typeNameFormatters);

        // Act
        var actual = sut.Process(request, callback);
        var code = callback.Builder.ToString();

        // Assert
        actual.Should().BeTrue();
        code.Should().Be(@"new CustomList<String>(new[]
{
    @""a"",
    @""b"",
    @""c"",
} )");
    }

    private class CustomList<T> : List<T>
    {
        public CustomList()
        {
        }

        public CustomList(IEnumerable<T> collection) : base(collection)
        {
        }

        public CustomList(int capacity) : base(capacity)
        {
        }
    }
}
