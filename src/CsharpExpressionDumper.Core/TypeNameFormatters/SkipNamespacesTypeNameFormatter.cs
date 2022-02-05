namespace CsharpExpressionDumper.Core.TypeNameFormatters;

public class SkipNamespacesTypeNameFormatter : ITypeNameFormatter
{
    private readonly string[] _namespacesToAbbreviate;

    public SkipNamespacesTypeNameFormatter(IEnumerable<string> namspacesToAbbreviate)
        => _namespacesToAbbreviate = namspacesToAbbreviate.ToArray();

    public string? Format(string currentValue)
    {
        foreach (var ns in _namespacesToAbbreviate)
        {
            if (currentValue.GetNamespaceWithDefault() == ns)
            {
                return currentValue.GetClassName();
            }
        }

        return default; // let the default type name formatter handle this
    }
}
