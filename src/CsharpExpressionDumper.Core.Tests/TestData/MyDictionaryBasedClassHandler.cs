namespace CsharpExpressionDumper.Core.Tests.TestData;

public class MyDictionaryBasedClassHandler : ICustomTypeHandler
{
    private readonly string _variableName;

    public MyDictionaryBasedClassHandler(string variableName) => _variableName = variableName;

    public bool Process(CustomTypeHandlerRequest request, ICsharpExpressionDumperCallback callback)
    {
        if (request.Instance is MyDictionaryBasedClass myDictionaryBasedClass)
        {
            callback.ChainAppend($"var {_variableName} = new MyDictionaryBasedClass(")
                    .ChainAppendFormattedString(myDictionaryBasedClass.Custom1)
                    .ChainAppend(", ")
                    .ChainAppend(myDictionaryBasedClass.Custom2)
                    .ChainAppend(");");

            var level = request.Level + 1;
            foreach (var kvp in myDictionaryBasedClass)
            {
                callback.ChainAppendLine()
                        .ChainAppend("x.Add(")
                        .ChainProcessRecursive(kvp.Key, kvp.Key?.GetType(), level)
                        .ChainAppend(", ")
                        .ChainProcessRecursive(kvp.Value, kvp.Value?.GetType(), level)
                        .ChainAppend(");");
            }
            return true;
        }

        return false;
    }
}
