using System;
using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.Core.Extensions
{
    public static class CsharpExpressionDumperCallbackExtensions
    {
        public static ICsharpExpressionDumperCallback ChainProcessRecursive(this ICsharpExpressionDumperCallback callback, object instance, Type type, int level)
        {
            callback.ProcessRecursive(instance, type, level);
            return callback;
        }
        public static ICsharpExpressionDumperCallback ChainAppendSingleValue(this ICsharpExpressionDumperCallback callback, object value)
        {
            callback.AppendSingleValue(value);
            return callback;
        }
        public static ICsharpExpressionDumperCallback ChainAppendPrefix(this ICsharpExpressionDumperCallback callback)
        {
            callback.AppendPrefix();
            return callback;
        }
        public static ICsharpExpressionDumperCallback ChainAppendSuffix(this ICsharpExpressionDumperCallback callback)
        {
            callback.AppendSuffix();
            return callback;
        }
        public static ICsharpExpressionDumperCallback ChainAppend(this ICsharpExpressionDumperCallback callback, object value)
        {
            callback.Append(value);
            return callback;
        }
        public static ICsharpExpressionDumperCallback ChainAppendLine(this ICsharpExpressionDumperCallback callback, object value)
        {
            callback.AppendLine(value);
            return callback;
        }
        public static ICsharpExpressionDumperCallback ChainAppendLine(this ICsharpExpressionDumperCallback callback)
        {
            callback.AppendLine();
            return callback;
        }
        public static ICsharpExpressionDumperCallback ChainAppendFormattedString(this ICsharpExpressionDumperCallback callback, string value)
        {
            callback.AppendFormattedString(value);
            return callback;
        }
        public static ICsharpExpressionDumperCallback ChainAppendTypeName(this ICsharpExpressionDumperCallback callback, Type type)
        {
            callback.AppendTypeName(type);
            return callback;
        }
    }
}
