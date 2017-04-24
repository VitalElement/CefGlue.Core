using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Xilium.CefGlue;

namespace CefGlue.Avalonia
{
    internal sealed class AvaloniaCefBrowserProcessHandler : CefBrowserProcessHandler
    {
        IDisposable _current;
        private object schedule = new object();

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
                    delayMs = 1;
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
