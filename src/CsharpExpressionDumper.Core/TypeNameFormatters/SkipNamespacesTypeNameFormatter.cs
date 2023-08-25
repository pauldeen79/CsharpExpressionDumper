﻿namespace CsharpExpressionDumper.Core.TypeNameFormatters;

internal class SkipNamespacesTypeNameFormatter : ITypeNameFormatter
{
    private readonly string[] _namespacesToAbbreviate;

    public SkipNamespacesTypeNameFormatter(IEnumerable<string> namspacesToAbbreviate)
        => _namespacesToAbbreviate = namspacesToAbbreviate.ToArray();

    public string? Format(string currentValue)
    {
        var currentNamespace = currentValue.GetNamespaceWithDefault();
        var shouldAbbreviate = Array.Exists(_namespacesToAbbreviate, x => x == currentNamespace);
        return shouldAbbreviate
            ? currentValue.GetClassName()
            : default;
    }
}
