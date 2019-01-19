using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace CefGlue.Avalonia
{
    internal sealed class WpfCefDisplayHandler : CefDisplayHandler
    {
        AvaloniaCefBrowser _owner;

        public WpfCefDisplayHandler(AvaloniaCefBrowser owner)
        {
            if (owner == null) throw new ArgumentNullException("owner");

            _owner = owner;
        }

        //protected override void OnLoadingStateChange(CefBrowser browser, bool isLoading, bool canGoBack, bool canGoForward)
        //{
        //}

        protected override void OnAddressChange(CefBrowser browser, CefFrame frame, string url)
        {
        }

        protected override void OnTitleChange(CefBrowser browser, string title)
        {
        }

        protected override bool OnTooltip(CefBrowser browser, string text)
        {
            return _owner.OnTooltip(text);
        }

        protected override void OnStatusMessage(CefBrowser browser, string value)
        {
        }
    }
}
