using System;
using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class DateTimeHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is DateTime dateTime)
            {
                callback.AppendSingleValue($"new System.DateTime({dateTime.Year}, {dateTime.Month}, {dateTime.Day}, {dateTime.Hour}, {dateTime.Minute}, {dateTime.Second}, {dateTime.Millisecond}, DateTimeKind.{dateTime.Kind})");
                return true;
            }

            return false;
        }
    }
}
