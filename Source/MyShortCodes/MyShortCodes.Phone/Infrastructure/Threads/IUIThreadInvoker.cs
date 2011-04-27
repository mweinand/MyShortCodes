using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyShortCodes.Phone.Infrastructure.Threads
{
    public interface IUIThreadInvoker
    {
        void Invoke(Action action);
    }
}
