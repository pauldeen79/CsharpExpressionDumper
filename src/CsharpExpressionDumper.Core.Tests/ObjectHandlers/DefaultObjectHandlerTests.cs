namespace CsharpExpressionDumper.Core.Tests.ObjectHandlers;

public class DefaultObjectHandlerTests
{
    [Fact]
    public void ProcessInstance_Returns_True_When_PropertyValue_On_Immutable_Instance_Is_Null()
    {
        // Arrange
        var sut = new DefaultObjectHandler();
        var instance = new MyImmutableClass("test");
        var command = new ObjectHandlerRequest(instance, typeof(MyImmutableClass), 0, typeof(MyImmutableClass), false);
        var callbackMock = new Mock<ICsharpExpressionDumperCallback>();

        // Act
        var actual = sut.ProcessInstance(command, callbackMock.Object);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void ProcessInstance_Returns_True_When_Instance_Is_Poco()
    {
        // Arrange
        var sut = new DefaultObjectHandler();
        var instance = new MyPocoClass();
        var command = new ObjectHandlerRequest(instance, typeof(MyPocoClass), 0, typeof(MyPocoClass), false);
        var callbackMock = new Mock<ICsharpExpressionDumperCallback>();

        // Act
        var actual = sut.ProcessInstance(command, callbackMock.Object);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void ProcessInstance_Returns_False_When_Instance_Is_Null()
    {
        // Arrange
        var sut = new DefaultObjectHandler();
        var instance = default(MyImmutableClass);
        var command = new ObjectHandlerRequest(instance, typeof(MyImmutableClass), 0, typeof(MyImmutableClass), false);
        var callbackMock = new Mock<ICsharpExpressionDumperCallback>();

        // Act
        var actual = sut.ProcessInstance(command, callbackMock.Object);

        // Assert
        actual.Should().BeFalse();
    }

    public class MyImmutableClass
    {
        public object? Property1 { get; }
        public MyImmutableClass(object? property1)
        {
            Property1 = property1;
        }
    }

    public class MyPocoClass
    {
        public object? Property1 { get; set; }
    }
}
