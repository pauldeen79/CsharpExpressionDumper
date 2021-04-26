using CsharpExpressionDumper.Abstractions.Requests;
using System.Reflection;

namespace CsharpExpressionDumper.Abstractions
{
    public interface IObjectHandlerPropertyFilter
    {
        bool IsValid(ObjectHandlerRequest command, PropertyInfo propertyInfo);
    }
}
