﻿namespace CsharpExpressionDumper.Core.Extensions;

internal static class EnumerableExtensions
{
    public static bool ProcessUntilSuccess<T>(this IEnumerable<T> instance, Func<T, bool> processDelegate)
        => instance.Any(x => processDelegate.Invoke(x));

    public static TResult? ProcessUntilSuccess<TInput, TResult>(this IEnumerable<TInput> instance, Func<TInput, TResult> processDelegate)
        where TResult : class?
    {
        foreach (var item in instance)
        {
            var result = processDelegate(item);
            if (result != default)
            {
                return result;
            }
        }

        return default;
    }
}
