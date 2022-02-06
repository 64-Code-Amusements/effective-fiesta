using System;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace LocalFunctions
{
    class Program
    {

        // entry point
        static void Main(string[] args)
        {
            Log.Information("Entering the `static main(~)`");
            var program = new Program();

            // standard local function
            {
                var lf = program.WithLocalFunction();
                Log.Information("Answer is {@Answer}", lf);
            }

            // function declared in a loop 
            {
                var lf = program.WithLocalFunctionInLoop();
                Log.Information("Answer is {@Answer}", lf);
            }

            // playing with delegates
            {
                var inc = program.GetIncrementor(1);
                var dec = program.GetIncrementor(-1);
                var up = inc(10);
                var down = dec(10);
                Log.Information("up() should be 11 and is {Up}", up);
                Log.Information("down() should be 9 and is {Down}", down);
            }

            // we are outta here
            Log.CloseAndFlush();
        }


        // static constructor
        static Program()
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    // .MinimumLevel.Verbose()
                    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                    .CreateLogger();
            }
            catch
            {
                Environment.Exit(-1);
            }
            Log.Information("In the static constructor");
        }

        Program()
        {
            Log.Information("In the default constructor");
        }

        private int WithLocalFunction()
        {
            var x = 7;
            var y = 11;

            return Add();

            int Add() => x + y;
        }

        private int WithLocalFunctionInLoop()
        {
            var x = 7;
            var y = 11;

            Func<int> Add_0_Lambda = () => x + y;
            Func<int, int> Add_1_Lambda = (z) => z + x;

            for (var i = 0; i < 5; i++)
            {
                Log.Information("Lambda {Add}", Add_0_Lambda());
                Log.Information("Local {Add}", AddLocal());
                y = i;
            }
            return Add_0_Lambda();

            // int AddLocal() => Add_0_Lambda();// x + y;
            int AddLocal() => Add_1_Lambda(y);

        }

        private Func<int, int> GetIncrementor(int step)
            => x => x + step;
        // {
        //     return x => x + step;

        //     // var _step = step;
        //     // Func<int, int> lambdaFunc = x => x + _step;
        //     // return lambdaFunc;

        // }

        /*
            Log.Verbose("A Verbose message");
            Log.Debug("A Debug message");
            Log.Information("A Information message");
            Log.Warning("A Warning message");
            Log.Error("A Error message");
            Log.Fatal("A Fatal message");
        */
    }
}
