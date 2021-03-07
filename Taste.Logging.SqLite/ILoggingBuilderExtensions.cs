namespace Taste.Logging.SqLite
{
    using Microsoft.Extensions.Logging;
    using System.Data.SQLite;

    public static class ILoggingBuilderExtensions
    {
        public static ILoggingBuilder AddSqLite(this ILoggingBuilder builder, SqLiteLoggerOptions loggerOptions)
        {
            var dbConnection = new SQLiteConnection(loggerOptions.ConnectionString);
            var loggerProvider = new SqLiteLoggerProvider(dbConnection);
            builder.AddProvider(loggerProvider);
            return builder;
        }
    }
}