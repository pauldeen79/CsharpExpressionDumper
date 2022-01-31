namespace CsharpExpressionDumper.Core.TypeNameFormatters;

public class SkipNamespacesTypeNameFormatter : ITypeNameFormatter
{
    private readonly string[] _namespacesToAbbreviate;

    public SkipNamespacesTypeNameFormatter(IEnumerable<string> namspacesToAbbreviate)
        => _namespacesToAbbreviate = namspacesToAbbreviate.ToArray();

    public string? Format(Type type)
    {
        var result = type.FullName.FixTypeName();

        foreach (var ns in _namespacesToAbbreviate)
        {
            if (result.GetNamespaceWithDefault() == ns)
            {
                return result.GetClassName();
            }
        }

        return default; // let the default type name formatter handle this
    }
}
