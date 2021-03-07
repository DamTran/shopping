namespace Taste.Logging.SqLite
{
    using Dapper;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Data;
    using System.Text.Json;

    public class SqLiteLogger : ILogger
    {
        private const string LogPattern = "INSERT INTO ApplicationEvent (Value, CreatedDate) VALUES (@Value, @CreatedDate)";
        private readonly IDbConnection _dbConnection;
        private readonly LogLevel _logLevel;
        private readonly EventId _eventId;
        private readonly string _categoryName;

        public SqLiteLogger(string categoryName, LogLevel logLevel, IDbConnection dbConnection)
        {
            _logLevel = logLevel;
            _categoryName = categoryName;
            _dbConnection = dbConnection;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logLevel < logLevel;
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            object arguments;
            if (_eventId == 0/* || _eventId == eventId.Id*/)
            {
                arguments = new
                {
                    Category = _categoryName,
                    LogLevel = logLevel,
                    Message = formatter(state, exception),
                };
            }
            else
            {
                arguments = new
                {
                    Category = _categoryName,
                    LogLevel = logLevel,
                    EventId = eventId,
                    Message = formatter(state, exception),
                };
            }

            string json = JsonSerializer.Serialize(arguments);

            using (_dbConnection)
            {
                _dbConnection.Open();

                await _dbConnection.ExecuteAsync(LogPattern, new
                {
                    Value = json,
                    CreatedDate = DateTime.UtcNow,
                });
            }
        }
    }
}