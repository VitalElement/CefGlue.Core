using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CefGlue.Samples.Avalonia
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.AttachDevTools();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
