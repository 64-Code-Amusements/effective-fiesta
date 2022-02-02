using System;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace LocalFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            log.Verbose("A Verbose message");
            log.Debug("A Debug message");
            log.Information("A Information message");
            log.Warning("A Warning message");
            log.Error("A Error message");
            log.Fatal("A Fatal message");
        }
    }
}
