using Avalonia;
using Avalonia.Markup.Xaml;

namespace CefGlue.Samples.Avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
