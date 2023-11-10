namespace CsharpExpressionDumper.Core.Tests.TypeNameFormatters;

public class SkipNamespacesTypeNameFormatterTests
{
    [Fact]
    public void Format_Returns_Null_When_Namespace_Is_Not_Known()
    {
        // Arrange
        var sut = new SkipNamespacesTypeNameFormatter(new[] { "WrongPrefix" });

        // Act
        var actual = sut.Format("System.String");

        // Assert
        actual.Should().BeNull();
    }

    [Fact]
    public void Format_Returns_Null_When_Input_Is_Null()
    {
        // Arrange
        var sut = new SkipNamespacesTypeNameFormatter(new[] { "WrongPrefix" });

        // Act
        var actual = sut.Format(null!);

        // Assert
        actual.Should().BeNull();
    }

    [Fact]
    public void Format_Returns_Value_Without_Namespace_When_Namespace_Is_Known()
    {
        // Arrange
        var sut = new SkipNamespacesTypeNameFormatter(new[] { "System" });

        // Act
        var actual = sut.Format("System.String");

        // Assert
        actual.Should().Be("String");
    }
}
