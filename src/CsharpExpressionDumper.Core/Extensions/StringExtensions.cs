namespace CsharpExpressionDumper.Core.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Fixes the name of the type.
    /// </summary>
    /// <param name="instance">The type name to fix.</param>
    /// <returns></returns>
    public static string FixTypeName(this string instance)
    {
        int startIndex;
        while (true)
        {
            startIndex = instance.IndexOf(", ");
            if (startIndex == -1)
            {
                break;
            }

            int secondIndex = instance.IndexOf("]", startIndex + 1);
            if (secondIndex == -1)
            {
                break;
            }

            instance = instance.Substring(0, startIndex) + instance.Substring(secondIndex + 1);
        }

        while (true)
        {
            startIndex = instance.IndexOf("`");
            if (startIndex == -1)
            {
                break;
            }

            instance = instance.Substring(0, startIndex) + instance.Substring(startIndex + 2);
        }

        //remove assebmly name from type, e.g. System.String, mscorlib bla bla bla -> System.String
        var index = instance.IndexOf(", ");
        if (index > -1)
        {
            instance = instance.Substring(0, index);
        }

        return instance
            .Replace("[[", "<")
            .Replace(",[", ",")
            .Replace(",]", ">")
            .Replace("]", ">")
            .Replace("[>", "[]") //hacking here! caused by the previous statements...
            .Replace("System.Void", "void")
            .Replace("+", ".")
            .Replace("&", "");
    }

    public static string GetClassName(this string fullyQualifiedClassName)
    {
        var idx = fullyQualifiedClassName.LastIndexOf(".");
        return idx == -1
            ? fullyQualifiedClassName
            : fullyQualifiedClassName.Substring(idx + 1);
    }

    public static string GetNamespaceWithDefault(this string fullyQualifiedClassName, string defaultValue = "")
    {
        var idx = fullyQualifiedClassName.LastIndexOf(".");
        return idx == -1
            ? defaultValue
            : fullyQualifiedClassName.Substring(0, idx);
    }
}
