namespace CsharpExpressionDumper.Core.Tests.ObjectHandlers;

public class BuilderObjectHandlerTests
{
    [Theory, AutoMockData]
    public void Can_Generate_Code_With_CustomObjectHandler([Frozen] ICsharpExpressionDumperCallback callback)
    {
        // Arrange
        var sut = new BuilderObjectHandler();
        var instance = new MyBuilder().WithName("Test").AddValues("1", "2", "3");
        var command = new ObjectHandlerRequest(instance, typeof(MyBuilder), 0, typeof(MyBuilder), false);
        callback.IsPropertyValid(Arg.Any<ObjectHandlerRequest>(), Arg.Any<PropertyInfo>()).Returns(true);

        // Act
        var actual = sut.ProcessInstance(command, callback);

        // Assert
        actual.Should().BeTrue();
    }

    [Theory, AutoMockData]
    public void ProcessInstance_Returns_False_On_Null_Type([Frozen] ICsharpExpressionDumperCallback callback)
    {
        // Arrange
        var sut = new BuilderObjectHandler();
        object? instance = null;
        var command = new ObjectHandlerRequest(instance, null, 0, null, false);
        callback.IsPropertyValid(Arg.Any<ObjectHandlerRequest>(), Arg.Any<PropertyInfo>()).Returns(true);

        // Act
        var actual = sut.ProcessInstance(command, callback);

        // Assert
        actual.Should().BeFalse();
    }

    [Theory, AutoMockData]
    public void ProcessInstance_Returns_False_On_Poco([Frozen] ICsharpExpressionDumperCallback callback)
    {
        // Arrange
        var sut = new BuilderObjectHandler();
        var instance = new MyPocoClass();
        var command = new ObjectHandlerRequest(instance, typeof(MyPocoClass), 0, typeof(MyPocoClass), false);

        // Act
        var actual = sut.ProcessInstance(command, callback);

        // Assert
        actual.Should().BeFalse();
    }

    public class MyPocoClass
    {
        public object? Property1 { get; set; }
    }
}
