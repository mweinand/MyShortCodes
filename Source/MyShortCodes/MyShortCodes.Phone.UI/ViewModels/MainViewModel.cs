using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using MyShortCodes.Phone.Infrastructure;
using MyShortCodes.Phone.Domain;

namespace MyShortCodes.Phone.UI.ViewModels
{
    public interface IMainViewModel
    {
        ObservableCollection<ShortCode> AllShortCodes { get; }
    }

    public class MainViewModel : BindableObject, IMainViewModel
    {
        public MainViewModel()
        {
            this.AllShortCodes = new ObservableCollection<ShortCode>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ShortCode> AllShortCodes { get; private set; }

    }
}