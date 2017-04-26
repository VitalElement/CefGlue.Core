using Xilium.CefGlue;

namespace CefGlue.Avalonia
{
    public class AvaloniaCefRenderProcessHandler : CefRenderProcessHandler
    {    
        protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        {
            if(message.Name == "executeJs")
            {                
                var context = browser.GetMainFrame().V8Context;

                context.TryEval(message.Arguments.GetString(0), message.Arguments.GetString(1), 1, out CefV8Value value, out CefV8Exception exception);

                var response = CefProcessMessage.Create("executeJsResult");

                if (value.IsString)
                {
                    response.Arguments.SetString(0, value.GetStringValue());
                }
                browser.SendProcessMessage(CefProcessId.Browser, response);
                return true;
            }

            return false;
        }
    }
}
