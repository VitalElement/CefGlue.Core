using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace CefGlue.Avalonia
{
    internal sealed class WpfCefLifeSpanHandler : CefLifeSpanHandler
    {
        private readonly AvaloniaCefBrowser _owner;

        public WpfCefLifeSpanHandler(AvaloniaCefBrowser owner)
        {
            if (owner == null) throw new ArgumentNullException("owner");

            _owner = owner;
        }

        protected override void OnAfterCreated(CefBrowser browser)
        {
            _owner.HandleAfterCreated(browser);
        }
    }
}
