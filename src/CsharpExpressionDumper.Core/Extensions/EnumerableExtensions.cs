using System;
using System.Collections.Generic;

namespace CsharpExpressionDumper.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool ProcessUntilSuccess<T>(this IEnumerable<T> instance, Func<T, bool> processDelegate)
        {
            foreach (var item in instance)
            {
                if (processDelegate(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static TResult ProcessUntilSuccess<TInput, TResult>(this IEnumerable<TInput> instance, Func<TInput, TResult> processDelegate)
            where TResult : class
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
}
