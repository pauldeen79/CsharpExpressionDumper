namespace CsharpExpressionDumper.Core.Tests;

public abstract class TestBase
{
    protected IFixture Fixture { get; } = new Fixture().Customize(new AutoNSubstituteCustomization());
}
