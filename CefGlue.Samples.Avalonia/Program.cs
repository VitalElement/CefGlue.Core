using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging.Serilog;
using Avalonia.Platform;
using Serilog;
using Xilium.CefGlue;
using System.IO;
using Avalonia.Threading;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace ControlCatalog
{
    internal sealed class AvaloniaCefBrowserProcessHandler : CefBrowserProcessHandler
    {
        private TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private CancellationTokenSource cts = new CancellationTokenSource();
        IDisposable _current;
        private object schedule = new object();
        private Thread uiThread = Thread.CurrentThread;

        protected override void OnScheduleMessagePumpWork(long delayMs)
        {
            lock (schedule)
            {
                if (_current != null)
                {
                    _current.Dispose();
                }

                if (delayMs <= 0)
                {
                    delayMs = 10;
                }

                _current = Observable.Interval(TimeSpan.FromMilliseconds(delayMs)).ObserveOn(AvaloniaScheduler.Instance).Subscribe((i) =>
               {
                   CefRuntime.DoMessageLoopWork();
                   
               });
            }
        }
    }

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

        private CefBrowserProcessHandler _browserProcessHandler;

        protected override CefBrowserProcessHandler GetBrowserProcessHandler()
        {
            if (_browserProcessHandler == null)
            {
                _browserProcessHandler = new AvaloniaCefBrowserProcessHandler();
            }

            return _browserProcessHandler;

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            AppBuilder.Configure<App>()
                .UseSkia().UseWin32().AfterSetup((a)=> 
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
                        MultiThreadedMessageLoop = false,
                        LogSeverity = CefLogSeverity.Verbose,
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
                }).Start<MainWindow>();            
        }
    }
}
