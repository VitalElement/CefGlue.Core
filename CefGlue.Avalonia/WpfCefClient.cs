using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace CefGlue.Avalonia
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public CefBrowser Browser { get; set; }
        public CefProcessId ProcessId { get; set; }
        public CefProcessMessage Message { get; set; }
    }

    public class WpfCefClient : CefClient
    {
        private AvaloniaCefBrowser _owner;

        private WpfCefLifeSpanHandler _lifeSpanHandler;
        private WpfCefDisplayHandler _displayHandler;
        private WpfCefRenderHandler _renderHandler;
        private WpfCefLoadHandler _loadHandler;
        private WpfCefJSDialogHandler _jsDialogHandler;

        public WpfCefClient(AvaloniaCefBrowser owner)
        {
            if (owner == null) throw new ArgumentNullException("owner");

            _owner = owner;

            _lifeSpanHandler = new WpfCefLifeSpanHandler(owner);
            _displayHandler = new WpfCefDisplayHandler(owner);
            _renderHandler = new WpfCefRenderHandler(owner, new UiHelper());
            _loadHandler = new WpfCefLoadHandler(owner);
            _jsDialogHandler = new WpfCefJSDialogHandler();
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        protected override CefLifeSpanHandler GetLifeSpanHandler()
        {
            return _lifeSpanHandler;
        }

        protected override CefDisplayHandler GetDisplayHandler()
        {
            return _displayHandler;
        }

        protected override CefRenderHandler GetRenderHandler()
        {
            return _renderHandler;
        }

        protected override CefLoadHandler GetLoadHandler()
        {
            return _loadHandler;
        }

        protected override CefJSDialogHandler GetJSDialogHandler()
        {
            return _jsDialogHandler;
        }

        protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs() { Browser = browser, ProcessId = sourceProcess, Message = message });

            return base.OnProcessMessageReceived(browser, sourceProcess, message);
        }
    }

}
