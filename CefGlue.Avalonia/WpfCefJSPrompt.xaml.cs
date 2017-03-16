using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CefGlue.Avalonia
{
    /// <summary>
    /// Interaction logic for WpfCefJSPrompt.xaml
    /// </summary>
    public partial class WpfCefJSPrompt : Window
    {
        public string Input { get { return null;/* return this.inputTextBox.Text;*/ } }

        public WpfCefJSPrompt(string message, string defaultText)
        {
           /* InitializeComponent();
            this.messageTextBlock.Text = message;
            this.inputTextBox.Text = defaultText;*/
        }

        /*private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }*/
    }
}
