namespace Taste.Logging.LiteDB
{
    using global::LiteDB;
    using Microsoft.Extensions.Logging;
    using System;

    public class LiteDBLogger : ILogger
    {
        private readonly string _connectionString;
        private readonly LogLevel _logLevel;
        private readonly string _categoryName;

        public LiteDBLogger(string categoryName, LogLevel logLevel, string connectionString)
        {
            _logLevel = logLevel;
            _categoryName = categoryName;
            _connectionString = connectionString;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logLevel < logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            // New event instance
            ApplicationEvent applicationEvent;
            if (eventId != 0)
            {
                applicationEvent = new ApplicationEvent
                {
                    CreatedDate = DateTime.UtcNow,
                    Category = _categoryName,
                    LogLevel = logLevel,
                    Message = formatter(state, exception),
                };
            }
            else
            {
                applicationEvent = new ApplicationEvent
                {
                    CreatedDate = DateTime.UtcNow,
                    Category = _categoryName,
                    LogLevel = logLevel,
                    EventId = eventId.Id,
                    EventName = eventId.Name,
                    Message = formatter(state, exception),
                };
            }

            using (var database = new LiteDatabase(_connectionString))
            {
                // Get a collection (or create, if doesn't exist)
                var eventCollection = database.GetCollection<ApplicationEvent>("ApplicationEvents");

                // Insert new event document (Id will be auto-incremented)
                eventCollection.Insert(applicationEvent);

                // Index document using Category, LogLevel and CreatedDate properties
                eventCollection.EnsureIndex(e => e.CreatedDate);
                eventCollection.EnsureIndex(e => e.Category);
                eventCollection.EnsureIndex(e => e.LogLevel);
                eventCollection.EnsureIndex(e => e.EventId);
                eventCollection.EnsureIndex(e => e.EventName);
            }
        }

        private class ApplicationEvent
        {
            public DateTime CreatedDate { get; set; }
            public string Category { get; set; }
            public LogLevel LogLevel { get; set; }
            public int EventId { get; set; }
            public string EventName { get; set; }
            public string Message { get; set; }
        }
    }
}