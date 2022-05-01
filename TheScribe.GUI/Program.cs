using System;
using Avalonia;
using Serilog;
using TheScribe.GUI.Views;

namespace TheScribe.GUI
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();
            
            Log.Information("Application started!");
            
            try
            {
                AppBuilder app = BuildAvaloniaApp();
                app.StartWithClassicDesktopLifetime(args);
            }
            catch (Exception e)
            {
                NoteDialog dialog = new NoteDialog("A fatal error occurred, please report this on GitHub!");
                dialog.Show();
                Log.Fatal(e, "Application crashed!");
            }
            
            Log.Information("Application closed!");
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}