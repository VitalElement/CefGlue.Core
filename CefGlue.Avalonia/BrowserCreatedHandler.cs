using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace CefGlue.Avalonia
{
    public class BrowserCreatedEventArgs : EventArgs
    {
        public BrowserCreatedEventArgs(CefBrowser browser)
        {
            Browser = browser;
        }

        public CefBrowser Browser { get; private set; }
    }

    public delegate void BrowserCreatedHandler(object sender, BrowserCreatedEventArgs e);
}
