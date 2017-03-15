using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging.Serilog;
using Avalonia.Platform;
using Serilog;

namespace ControlCatalog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TODO: Make this work with GTK/Skia/Cairo depending on command-line args
            // again.
            AppBuilder.Configure<App>()
                .UseSkia().UseWin32()
                .Start<MainWindow>();
        }
    }
}
