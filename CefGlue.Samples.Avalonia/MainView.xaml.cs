using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Xilium.CefGlue;

namespace CefGlue.Samples.Avalonia
{
    public class MainView : UserControl
    {
        public MainView()
        {
            this.InitializeComponent();            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
