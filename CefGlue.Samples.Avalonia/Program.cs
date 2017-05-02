using Avalonia;
using CefGlue.Avalonia;

namespace CefGlue.Samples.Avalonia
{    
    internal class Program
    {
        static void Main(string[] args)
        {
            AppBuilder.Configure<App>()
                .UseSkia().UseWin32().ConfigureCefGlue(args).Start<MainWindow>();
        }
    }
}
