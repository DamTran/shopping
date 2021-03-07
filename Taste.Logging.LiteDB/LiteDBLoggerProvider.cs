namespace Taste.Logging.LiteDB
{
    using Microsoft.Extensions.Logging;
    using System.Collections.Concurrent;

    public sealed class LiteDBLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, LiteDBLogger> _loggers = new ConcurrentDictionary<string, LiteDBLogger>();
        private readonly string _connectionString;

        public LiteDBLoggerProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new LiteDBLogger(name, LogLevel.Information, _connectionString));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}