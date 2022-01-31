namespace CsharpExpressionDumper.Abstractions;

public interface IObjectHandler
{
    bool ProcessInstance(ObjectHandlerRequest command, ICsharpExpressionDumperCallback callback);
}
