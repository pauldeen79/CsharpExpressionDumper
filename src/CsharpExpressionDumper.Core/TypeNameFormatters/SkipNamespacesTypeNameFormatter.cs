namespace CsharpExpressionDumper.Core.TypeNameFormatters;

public class SkipNamespacesTypeNameFormatter : ITypeNameFormatter
{
    private readonly string[] _namespacesToAbbreviate;

    public SkipNamespacesTypeNameFormatter(IEnumerable<string> namspacesToAbbreviate)
        => _namespacesToAbbreviate = namspacesToAbbreviate.ToArray();

    public string? Format(string currentValue)
    {
        if (currentValue is null)
        {
            return null;
        }

        var currentNamespace = currentValue.GetNamespaceWithDefault();
        var shouldAbbreviate = Array.Exists(_namespacesToAbbreviate, x => x == currentNamespace);

        return shouldAbbreviate
            ? currentValue.GetClassName()
            : default;
    }
}
