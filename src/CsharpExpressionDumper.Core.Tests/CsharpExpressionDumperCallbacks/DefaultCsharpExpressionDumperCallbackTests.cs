namespace CsharpExpressionDumper.Core.Tests.CsharpExpressionDumperCallbacks;

public class DefaultCsharpExpressionDumperCallbackTests
{
    [Fact]
    public void AppendTypeName_Throws_When_No_TypeNameFormatter_Returns_A_NonEmpty_Result()
    {
        // Arrange
        var sut = new DefaultCsharpExpressionDumperCallback(Enumerable.Empty<ICustomTypeHandler>(),
                                                            Enumerable.Empty<ITypeNameFormatter>(),
                                                            Enumerable.Empty<IConstructorResolver>(),
                                                            Enumerable.Empty<IReadOnlyPropertyResolver>(),
                                                            Enumerable.Empty<IObjectHandlerPropertyFilter>());

        // Act & Assert
        sut.Invoking(x => x.AppendTypeName(typeof(string)))
           .Should().ThrowExactly<ArgumentException>()
           .WithMessage("Typename of type [System.String] could not be formatted");
    }
}
