using System;

namespace Logger.Console
{
    public class ConsoleMessageArgumentOutOfRangeException : ArgumentOutOfRangeException
    {
        public ConsoleMessageArgumentOutOfRangeException(string message) : base(message)
        {
        }

        public ConsoleMessageArgumentOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
