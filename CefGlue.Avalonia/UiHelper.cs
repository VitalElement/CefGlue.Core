using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CefGlue.Avalonia
{
    public class UiHelper : IUiHelper
    {    
        public void PerformInUiThread(Action action)
        {
            Dispatcher.UIThread.InvokeAsync(action);
        }

        public void StartAsynchronously(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            Task.Factory.StartNew(action);            
        }

        public void PerformForMinimumTime(Action action, bool requiresUiThread, int minimumMillisecondsBeforeReturn)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            var startTime = Environment.TickCount;

            if (requiresUiThread)
            {
                PerformInUiThread(action);
            }
            else
            {
                action.Invoke();
            }

            var remainingTime = minimumMillisecondsBeforeReturn - (Environment.TickCount - startTime);

            if (remainingTime > 0)
            {
                //Thread.Sleep(remainingTime);
            }
        }

        public void IgnoreException(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {                
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns>Default if fails</returns>
        public T IgnoreException<T>(Func<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            try
            {
                return action.Invoke();
            }
            catch (Exception e)
            {                
                return default(T);
            }
        }

        public void Sleep(int milliseconds)
        {
           // Thread.Sleep(milliseconds);
        }

        public void Sleep(TimeSpan sleepTime)
        {
           // Thread.Sleep(sleepTime);
        }
    }

}
