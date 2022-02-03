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
        var request = CreateRequest(instance);
        var typeHandlers = new[] { new StringHandler() };
        var callback = CreateCallback(typeHandlers, typeNameFormatters);

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
    public void Can_Process_Generic_List_With_Abbreviated_TypeName()
    {
        // Arrange
        var typeNameFormatters = new[] { new SkipNamespacesTypeNameFormatter(new[] { "System", "System.Collections.Generic" }) };
        var sut = new EnumerableHandler(typeNameFormatters);
        var instance = new List<string> { "a", "b", "c" };
        var request = CreateRequest(instance);
        var typeHandlers = new[] { new StringHandler() };
        var callback = CreateCallback(typeHandlers, typeNameFormatters);

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

    private static CustomTypeHandlerRequest CreateRequest(List<string> instance)
        => new CustomTypeHandlerRequest(instance, instance.GetType(), 0);

    private static DefaultCsharpExpressionDumperCallback CreateCallback(ICustomTypeHandler[] typeHandlers,
                                                                        ITypeNameFormatter[] typeNameFormatters)
    {
        var callback = new DefaultCsharpExpressionDumperCallback
        (
            typeHandlers,
            typeNameFormatters,
            Enumerable.Empty<IConstructorResolver>(),
            Enumerable.Empty<IReadOnlyPropertyResolver>(),
            Enumerable.Empty<IObjectHandlerPropertyFilter>()
        );
        //little hacky... this initializes the ProcessRecursiveCallbackDelegate property on the callback class
        callback.ProcessRecursiveCallbackDelegate = new Action<object?, Type?, StringBuilder, int>((instance, type, builder, level) => typeHandlers.ProcessUntilSuccess(x => x.Process(new CustomTypeHandlerRequest(instance, type, level), callback)));
        return callback;
    }
}
