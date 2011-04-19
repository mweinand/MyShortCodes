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

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            this.AllShortCodes.Add(new ShortCodeModel { ShortCodeId = 1, Code = "12345", Name = "Sample Code 1" });
            this.AllShortCodes.Add(new ShortCodeModel { ShortCodeId = 2, Code = "23456", Name = "Sample Code 2" });
            this.AllShortCodes.Add(new ShortCodeModel { ShortCodeId = 3, Code = "34567", Name = "Sample Code 3" });

            this.IsDataLoaded = true;
        }

    }
}