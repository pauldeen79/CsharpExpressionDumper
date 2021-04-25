using CsharpExpressionDumper.Abstractions.Commands;
using System;
using System.Reflection;
using System.Text;

namespace CsharpExpressionDumper.Abstractions
{
    public interface ICsharpExpressionDumperCallback
    {
        Action<object?, Type?, StringBuilder, int> ProcessRecursiveCallbackDelegate { get; set; }
        StringBuilder Builder { get; set; }
        void ProcessRecursive(object? instance, Type? type, int level);
        void AppendSingleValue(object value);
        void AppendPrefix();
        void AppendSuffix();
        void Append(object value);
        void AppendLine(object value);
        void AppendLine();
        void AppendFormattedString(string value);
        void AppendTypeName(Type type);
        bool IsPropertyCustom(CustomTypeHandlerCommand propertyCommand, string prefix, string suffix);
        bool IsPropertyValid(ObjectHandlerCommand command, PropertyInfo propertyInfo);
        ConstructorInfo? ResolveConstructor(Type type);
        PropertyInfo? ResolveReadOnlyProperty(PropertyInfo[] properties, ConstructorInfo ctor, ParameterInfo argument);
    }
}
