using CsharpExpressionDumper.Abstractions.Commands;
using System.Reflection;

namespace CsharpExpressionDumper.Abstractions
{
    public interface IObjectHandlerPropertyFilter
    {
        bool IsValid(ObjectHandlerCommand command, PropertyInfo propertyInfo);
    }
}
