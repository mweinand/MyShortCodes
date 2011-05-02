using System;
using System.Windows;
using Mangifera.Threading;

namespace MyShortCodes.Phone.UI.Infrastructure.Threads
{
    public class UIThreadInvoker : IUIThreadInvoker
    {
        public void Invoke(Action action)
        {
            ((App)Application.Current).RootFrame.Dispatcher.BeginInvoke(action);
        }
    }
}
