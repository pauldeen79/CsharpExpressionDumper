namespace CsharpExpressionDumper.Core.Tests.CsharpExpressionDumperCallbacks;

public class DefaultCsharpExpressionDumperCallbackTests
{
    [Fact]
    public void AppendTypeName_Returns_Value_Unchanged_When_No_TypeNameFormatter_Returns_A_NonEmpty_Result()
    {
        // Arrange
        var sut = new DefaultCsharpExpressionDumperCallback(Enumerable.Empty<ICustomTypeHandler>(),
                                                            Enumerable.Empty<ITypeNameFormatter>(),
                                                            Enumerable.Empty<IConstructorResolver>(),
                                                            Enumerable.Empty<IReadOnlyPropertyResolver>(),
                                                            Enumerable.Empty<IObjectHandlerPropertyFilter>());

        // Act
        sut.AppendTypeName(typeof(string));

        // Assert
        sut.Builder.ToString().Should().Be("System.String");
    }
}
