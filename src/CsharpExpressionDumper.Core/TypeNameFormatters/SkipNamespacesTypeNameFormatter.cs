namespace CsharpExpressionDumper.Core.TypeNameFormatters;

public class SkipNamespacesTypeNameFormatter : ITypeNameFormatter
{
    private readonly string[] _namespacesToAbbreviate;

    public SkipNamespacesTypeNameFormatter(IEnumerable<string> namspacesToAbbreviate)
        => _namespacesToAbbreviate = namspacesToAbbreviate.ToArray();

    public string? Format(string currentValue)
    {
        var currentNamespace = currentValue.GetNamespaceWithDefault();
        var shouldAbbreviate = _namespacesToAbbreviate.Any(x => x == currentNamespace);
        return shouldAbbreviate
            ? currentValue.GetClassName()
            : default;
    }
}
