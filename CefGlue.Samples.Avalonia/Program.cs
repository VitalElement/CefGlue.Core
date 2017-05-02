using Avalonia;
using CefGlue.Avalonia;

namespace CefGlue.Samples.Avalonia
{    
    internal class Program
    {
        static void Main(string[] args)
        {
            AppBuilder.Configure<App>()
                .UsePlatformDetect().UseSkia().ConfigureCefGlue(args).Start<MainWindow>();
        }
    }
}
