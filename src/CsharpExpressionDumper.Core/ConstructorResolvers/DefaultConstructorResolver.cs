namespace CsharpExpressionDumper.Core.ConstructorResolvers;

internal class DefaultConstructorResolver : IConstructorResolver
{
    public ConstructorInfo? Resolve(Type type)
    {
        var ctors = type.GetConstructors();
        if (ctors.Length == 1)
        {
            return ctors[0];
        }

        return null;
    }
}
