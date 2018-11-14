using System;

namespace RuslanSh.PostSharpLogging.Test.Infrastructure.Logging
{
    internal class ConsoleLogger : ILogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
