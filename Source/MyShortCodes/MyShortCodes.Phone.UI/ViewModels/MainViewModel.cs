using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MyShortCodes.Phone.UI.ViewModels
{
    public class MainViewModel : BindableObject
    {
        public MainViewModel()
        {
            this.AllShortCodes = new ObservableCollection<ShortCodeModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ShortCodeModel> AllShortCodes { get; private set; }

        public bool IsDataLoaded
        {
            get;
            set;
        }
    }
}