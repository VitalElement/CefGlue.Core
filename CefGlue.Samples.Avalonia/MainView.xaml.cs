using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Xilium.CefGlue;

namespace ControlCatalog
{
    public class MainView : UserControl
    {
        public MainView()
        {
            this.InitializeComponent();
            CefBrowserHost.CreateBrowser()
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
