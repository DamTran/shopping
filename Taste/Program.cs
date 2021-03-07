namespace Taste
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Taste.Logging.LiteDB;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostContext, builder) =>
                {
                    builder.ClearProviders();

                    string connectionString = hostContext.Configuration.GetConnectionString("LiteDB");
                    builder.AddLiteDB(new LiteDBLoggerOptions
                    {
                        ConnectionString = connectionString,
                    });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //var port = Environment.GetEnvironmentVariable("PORT");
                    webBuilder.UseStartup<Startup>();
                    //.UseUrls("http://*:" + port);
                });
    }
}