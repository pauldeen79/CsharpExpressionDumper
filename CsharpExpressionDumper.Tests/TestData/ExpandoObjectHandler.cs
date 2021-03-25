using CsharpExpressionDumper.Abstractions;
using System.Dynamic;

namespace CsharpExpressionDumper.Tests.TestData
{
    public class ExpandoObjectHandler : ICustomTypeHandler
    {
        private readonly string _variableName;

        public ExpandoObjectHandler(string variableName)
        {
            _variableName = variableName;
        }

        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is ExpandoObject expandoObject)
            {
                callback.AppendLine($"dynamic {_variableName} = new System.Dynamic.ExpandoObject();");
                foreach (var keyValuePair in expandoObject)
                {
                    callback.Append(new string(' ', command.Level * 4));
                    callback.Append($"{_variableName}.{keyValuePair.Key} = ");
                    callback.ProcessRecursive(keyValuePair.Value, keyValuePair.Value?.GetType(), command.Level);
                    callback.AppendLine(";");
                }
                return true;
            }

            return false;
        }
    }
}
