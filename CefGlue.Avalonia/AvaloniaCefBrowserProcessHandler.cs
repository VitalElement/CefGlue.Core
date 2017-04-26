using Avalonia.Threading;
using System;
using System.Reactive.Linq;
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
}
