using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;

namespace WebAppTestLogging
{
    public class LoggingApp
    {
        public static int StartWith(Action startApp, IDictionary<string, LogEventLevel> overrides = null)
        {
            SelfLog.Enable(msg => Console.WriteLine(msg));

            var configuration = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext();

            if (overrides != null)
            {
                foreach (var overrride in overrides)
                {
                    configuration.MinimumLevel.Override(overrride.Key, overrride.Value);
                }
            }

            Log.Logger = configuration.CreateLogger();
            try
            {
                Log.Information("Starting web host");
                startApp();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Task.Delay(1000).Wait();
                Log.CloseAndFlush();
                Task.Delay(1000).Wait();
            }
        }
    }
}