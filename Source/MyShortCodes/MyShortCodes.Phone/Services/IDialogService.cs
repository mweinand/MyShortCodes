using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MyShortCodes.Phone.Services
{
    public interface IDialogService
    {
        bool Confirm(string message);
    }

    public class DialogService : IDialogService
    {
        public bool Confirm(string message)
        {
            var result = MessageBox.Show(message, "Confirm", MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK;
        }
    }
}
