using System;
using Serilog;

namespace TheScribe.Application.Tests.Fixtures;

public class LoggingFixture : IDisposable
{
    static LoggingFixture()
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console() // TODO: Is this the right kind of sink for a unit test? Need more like a /dev/null sink
            .CreateLogger();
    }
    
    public LoggingFixture()
    {

    }

    public void Dispose()
    {
        
    }
}