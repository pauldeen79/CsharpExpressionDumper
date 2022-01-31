namespace CsharpExpressionDumper.Core.Tests.TestData;

public class ExpandoObjectHandler : ICustomTypeHandler
{
    private readonly string _variableName;

    public ExpandoObjectHandler(string variableName) => _variableName = variableName;

    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (request.Instance is ExpandoObject expandoObject)
        {
            callback.AppendLine($"dynamic {_variableName} = new System.Dynamic.ExpandoObject();");
            foreach (var keyValuePair in expandoObject)
            {
                callback.Append(new string(' ', request.Level * 4));
                callback.Append($"{_variableName}.{keyValuePair.Key} = ");
                callback.ProcessRecursive(keyValuePair.Value, keyValuePair.Value?.GetType(), request.Level);
                callback.AppendLine(";");
            }
            return true;
        }

        return false;
    }
}
