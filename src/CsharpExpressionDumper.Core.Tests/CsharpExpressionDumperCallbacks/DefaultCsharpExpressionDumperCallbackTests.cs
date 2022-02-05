namespace CsharpExpressionDumper.Core.Tests.CsharpExpressionDumperCallbacks;

public class DefaultCsharpExpressionDumperCallbackTests
{
    [Fact]
    public void AppendTypeName_Returns_Value_Unchanged_When_No_TypeNameFormatter_Returns_A_NonEmpty_Result()
    {
        // Arrange
        var sut = new DefaultCsharpExpressionDumperCallback(Enumerable.Empty<ICustomTypeHandler>(),
                                                            new[] { new Mock<ITypeNameFormatter>().Object },
                                                            Enumerable.Empty<IConstructorResolver>(),
                                                            Enumerable.Empty<IReadOnlyPropertyResolver>(),
                                                            Enumerable.Empty<IObjectHandlerPropertyFilter>());

        // Act
        sut.AppendTypeName(typeof(string));

        // Assert
        sut.Builder.ToString().Should().Be("System.String");
    }

    [Fact]
    public void AppendTypeName_Returns_Value_Unchanged_When_No_TypeNameFormatters_Are_Registered()
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

    [Fact]
    public void AppendTypeName_Returns_Updated_Value_When_TypeNameFormatter_Returns_A_NonEmpty_Result()
    {
        // Arrange
        var typeNameFormatterMock = new Mock<ITypeNameFormatter>();
        typeNameFormatterMock.Setup(x => x.Format("System.String")).Returns("string");
        var sut = new DefaultCsharpExpressionDumperCallback(Enumerable.Empty<ICustomTypeHandler>(),
                                                            new[] { typeNameFormatterMock.Object },
                                                            Enumerable.Empty<IConstructorResolver>(),
                                                            Enumerable.Empty<IReadOnlyPropertyResolver>(),
                                                            Enumerable.Empty<IObjectHandlerPropertyFilter>());

        // Act
        sut.AppendTypeName(typeof(string));

        // Assert
        sut.Builder.ToString().Should().Be("string");
    }
}
