namespace CsharpExpressionDumper.Core.Tests.ObjectHandlers;

public class BuilderObjectHandlerTests
{
    [Fact]
    public void Can_Generate_Code_With_CustomObjectHandler()
    {
        // Arrange
        var sut = new BuilderObjectHandler();
        var instance = new MyBuilder().WithName("Test").AddValues("1", "2", "3");
        var command = new ObjectHandlerRequest(instance, typeof(MyBuilder), 0, typeof(MyBuilder), false);
        var callbackMock = new Mock<ICsharpExpressionDumperCallback>();
        callbackMock.Setup(x => x.IsPropertyValid(It.IsAny<ObjectHandlerRequest>(), It.IsAny<PropertyInfo>())).Returns(true);

        // Act
        var actual = sut.ProcessInstance(command, callbackMock.Object);

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
        var callbackMock = new Mock<ICsharpExpressionDumperCallback>();
        callbackMock.Setup(x => x.IsPropertyValid(It.IsAny<ObjectHandlerRequest>(), It.IsAny<PropertyInfo>())).Returns(true);

        // Act
        var actual = sut.ProcessInstance(command, callbackMock.Object);

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
        var callbackMock = new Mock<ICsharpExpressionDumperCallback>();

        // Act
        var actual = sut.ProcessInstance(command, callbackMock.Object);

        // Assert
        actual.Should().BeFalse();
    }

    public class MyPocoClass
    {
        public object? Property1 { get; set; }
    }
}
