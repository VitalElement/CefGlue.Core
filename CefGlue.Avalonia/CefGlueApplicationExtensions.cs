using Avalonia.Controls;
using System;
using Xilium.CefGlue;

namespace CefGlue.Avalonia
{
    public static class CefGlueApplicationExtensions
    {
        public static T ConfigureCefGlue<T>(this T builder, string[] args) where T : AppBuilderBase<T>, new()
        {
            return builder.AfterSetup((b) =>
            {
                try
                {
                    CefRuntime.Load();
                }
                catch (DllNotFoundException ex)
                {

                }
                catch (CefRuntimeException ex)
                {

                }
                catch (Exception ex)
                {

                }

                var mainArgs = new CefMainArgs(args);
                var cefApp = new SampleCefApp();

                var exitCode = CefRuntime.ExecuteProcess(mainArgs, cefApp);
                if (exitCode != -1) { return; }

                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(location);

                var cefSettings = new CefSettings
                {
                    SingleProcess = true,
                    WindowlessRenderingEnabled = true,
                    MultiThreadedMessageLoop = false,
                    LogSeverity = CefLogSeverity.Disable,
                    LogFile = "cef.log",
                    ExternalMessagePump = true
                };

                try
                {
                    CefRuntime.Initialize(mainArgs, cefSettings, cefApp);
                }
                catch (CefRuntimeException ex)
                {

                }
            });
        }
    }
}
