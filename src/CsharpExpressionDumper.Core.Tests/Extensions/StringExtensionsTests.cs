namespace CsharpExpressionDumper.Core.Tests.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData("System.Nullable`1[[System.Boolean, System.Private.CoreLib, Version=5.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]", "System.Nullable<System.Boolean>")]
    [InlineData("MyNamespace.MyClass+MySubClass", "MyNamespace.MyClass.MySubClass")]
    public void FixTypeName_Returns_Correct_Result(string input, string expectedResult)
    {
        // Act
        var actual = input.FixTypeName();

        // Assert
        actual.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("MyClass", "MyClass")]
    [InlineData("MyNamespace.MyClass", "MyClass")]
    [InlineData("A.B.C.D", "D")]
    public void GetClassName_Returns_Correct_Result(string input, string expectedResult)
    {
        // Act
        var actual = input.GetClassName();

        // Assert
        actual.Should().Be(expectedResult);
    }


    [Theory]
    [InlineData("", "")]
    [InlineData("MyClass", "")]
    [InlineData("MyNamespace.MyClass", "MyNamespace")]
    [InlineData("A.B.C.D", "A.B.C")]
    public void GetNamespaceWithDefault_Returns_Correct_Result(string input, string expectedResult)
    {
        // Act
        var actual = input.GetNamespaceWithDefault();

        // Assert
        actual.Should().Be(expectedResult);
    }
}
