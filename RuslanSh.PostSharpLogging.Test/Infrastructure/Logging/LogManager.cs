namespace RuslanSh.PostSharpLogging.Test.Infrastructure.Logging
{
    public static class LogManager
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }
        
        public static void WriteLine(string message)
        {
            _logger.WriteLine(message);
        }
    }
}
