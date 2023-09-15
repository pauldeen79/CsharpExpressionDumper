namespace CsharpExpressionDumper.Core.Tests.ObjectHandlers;

public class BuilderObjectHandlerTests : TestBase
{
    public ICsharpExpressionDumperCallback Callback { get; }

    public BuilderObjectHandlerTests()
    {
        Callback = Fixture.Freeze<ICsharpExpressionDumperCallback>();
    }

    [Fact]
    public void Can_Generate_Code_With_CustomObjectHandler()
    {
        // Arrange
        var sut = new BuilderObjectHandler();
        var instance = new MyBuilder().WithName("Test").AddValues("1", "2", "3");
        var command = new ObjectHandlerRequest(instance, typeof(MyBuilder), 0, typeof(MyBuilder), false);
        Callback.IsPropertyValid(Arg.Any<ObjectHandlerRequest>(), Arg.Any<PropertyInfo>()).Returns(true);

        // Act
        var actual = sut.ProcessInstance(command, Callback);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void ProcessInstance_Returns_False_On_Null_Type()
    {
        // Arrange
        var sut = new BuilderObjectHandler();
        object? instance = null;
        var command = new ObjectHandlerRequest(instance, null, 0, null, false);
        Callback.IsPropertyValid(Arg.Any<ObjectHandlerRequest>(), Arg.Any<PropertyInfo>()).Returns(true);

        // Act
        var actual = sut.ProcessInstance(command, Callback);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void ProcessInstance_Returns_False_On_Poco()
    {
        // Arrange
        var sut = new BuilderObjectHandler();
        var instance = new MyPocoClass();
        var command = new ObjectHandlerRequest(instance, typeof(MyPocoClass), 0, typeof(MyPocoClass), false);

        // Act
        var actual = sut.ProcessInstance(command, Callback);

        // Assert
        actual.Should().BeFalse();
    }

    public class MyPocoClass
    {
        public object? Property1 { get; set; }
    }
}
