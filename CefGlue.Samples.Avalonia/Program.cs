using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging.Serilog;
using Avalonia.Platform;
using Serilog;
using Xilium.CefGlue;
using System.IO;

namespace ControlCatalog
{
    internal sealed class SampleCefApp : CefApp
    {
        public SampleCefApp()
        {
        }

        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {
            if (string.IsNullOrEmpty(processType))
            {
                commandLine.AppendSwitch("disable-gpu");
                commandLine.AppendSwitch("disable-gpu-compositing");
                commandLine.AppendSwitch("enable-begin-frame-scheduling");
                commandLine.AppendSwitch("disable-smooth-scrolling");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
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
                BrowserSubprocessPath = Path.Combine(directory, "cefclient.exe"),
                SingleProcess = false,
                WindowlessRenderingEnabled = true,
                MultiThreadedMessageLoop = true,
                LogSeverity = CefLogSeverity.Disable,
                LogFile = "cef.log",
            };

            try
            {
                CefRuntime.Initialize(mainArgs, cefSettings, cefApp);
            }
            catch (CefRuntimeException ex)
            {
                
            }
            // TODO: Make this work with GTK/Skia/Cairo depending on command-line args
            // again.
            AppBuilder.Configure<App>()
                .UseSkia().UseWin32()
                .Start<MainWindow>();
        }
    }
}
