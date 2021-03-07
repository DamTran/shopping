namespace Taste.Logging.LiteDB
{
    using Microsoft.Extensions.Logging;

    public static class ILoggingBuilderExtensions
    {
        public static ILoggingBuilder AddLiteDB(this ILoggingBuilder builder, LiteDBLoggerOptions loggerOptions)
        {
            var loggerProvider = new LiteDBLoggerProvider(loggerOptions.ConnectionString);
            builder.AddProvider(loggerProvider);
            return builder;
        }
    }
}