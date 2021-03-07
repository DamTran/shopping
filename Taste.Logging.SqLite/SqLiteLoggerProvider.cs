namespace Taste.Logging.SqLite
{
    using Microsoft.Extensions.Logging;
    using System.Collections.Concurrent;
    using System.Data;

    public sealed class SqLiteLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, SqLiteLogger> _loggers = new ConcurrentDictionary<string, SqLiteLogger>();
        private readonly IDbConnection _dbConnection;

        public SqLiteLoggerProvider(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new SqLiteLogger(name, LogLevel.Information, _dbConnection));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}