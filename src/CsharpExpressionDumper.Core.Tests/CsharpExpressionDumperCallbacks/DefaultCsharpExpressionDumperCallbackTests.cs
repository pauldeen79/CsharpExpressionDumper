namespace CsharpExpressionDumper.Core.Tests.CsharpExpressionDumperCallbacks;

public class DefaultCsharpExpressionDumperCallbackTests : TestBase
{
    [Fact]
    public void AppendTypeName_Returns_Value_Unchanged_When_No_TypeNameFormatter_Returns_A_NonEmpty_Result()
    {
        // Arrange
        var typeNameFormatter = Fixture.Freeze<ITypeNameFormatter>();
        typeNameFormatter.Format(Arg.Any<string>()).Returns(default(string));
        var sut = new DefaultCsharpExpressionDumperCallback(Enumerable.Empty<ICustomTypeHandler>(),
                                                            new[] { typeNameFormatter },
                                                            Enumerable.Empty<IConstructorResolver>(),
                                                            Enumerable.Empty<IReadOnlyPropertyResolver>(),
                                                            Enumerable.Empty<IObjectHandlerPropertyFilter>());

        // Act
        sut.AppendTypeName(typeof(string));

        // Assert
        sut.Builder.ToString().ShouldBe("System.String");
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
        sut.Builder.ToString().ShouldBe("System.String");
    }

    [Fact]
    public void AppendTypeName_Returns_Updated_Value_When_TypeNameFormatter_Returns_A_NonEmpty_Result()
    {
        // Arrange
        var typeNameFormatterMock = Substitute.For<ITypeNameFormatter>();
        typeNameFormatterMock.Format("System.String").Returns("string");
        var sut = new DefaultCsharpExpressionDumperCallback(Enumerable.Empty<ICustomTypeHandler>(),
                                                            new[] { typeNameFormatterMock },
                                                            Enumerable.Empty<IConstructorResolver>(),
                                                            Enumerable.Empty<IReadOnlyPropertyResolver>(),
                                                            Enumerable.Empty<IObjectHandlerPropertyFilter>());

        // Act
        sut.AppendTypeName(typeof(string));

        // Assert
        sut.Builder.ToString().ShouldBe("string");
    }
}
