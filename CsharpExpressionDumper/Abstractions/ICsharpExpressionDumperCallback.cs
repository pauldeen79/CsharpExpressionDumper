using System;
using System.Reflection;

namespace CsharpExpressionDumper.Abstractions
{
    public interface ICsharpExpressionDumperCallback
    {
        void ProcessRecursive(object instance, Type type, int level);
        void AppendSingleValue(object value);
        void AppendPrefix();
        void AppendSuffix();
        void Append(object value);
        void AppendLine(object value);
        void AppendLine();
        void AppendFormattedString(string value);
        void AppendTypeName(Type type);
        ICsharpExpressionDumperCallback CreateNestedCallback(string prefix, string suffix);
        bool IsPropertyCustom(CustomTypeHandlerCommand propertyCommand, ICsharpExpressionDumperCallback propertyCallback);
        ConstructorInfo ResolveConstructor(Type type);
        PropertyInfo ResolveReadOnlyProperty(PropertyInfo[] properties, ConstructorInfo ctor, ParameterInfo argument);
    }
}