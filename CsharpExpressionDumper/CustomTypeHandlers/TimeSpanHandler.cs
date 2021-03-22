using System;
using CsharpExpressionDumper.Abstractions;

namespace CsharpExpressionDumper.CustomTypeHandlers
{
    public class TimeSpanHandler : ICustomTypeHandler
    {
        public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
        {
            if (command.Instance is TimeSpan timeSpan)
            {
                callback.AppendSingleValue($"new System.TimeSpan({timeSpan.Days}, {timeSpan.Hours}, {timeSpan.Minutes}, {timeSpan.Seconds}, {timeSpan.Milliseconds})");
                return true;
            }

            return false;
        }
    }
}
