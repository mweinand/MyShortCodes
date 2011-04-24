using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MyShortCodes.Phone.Domain;
using MyShortCodes.Phone.Infrastructure;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyShortCodes.Phone.ViewModels
{
    public interface IAddPageViewModel
    {
        ShortCode ActiveShortCode { get; set; }
        string PageTitle { get; set; }
        ObservableCollection<string> Errors { get; }
    }

    public class AddPageViewModel : BindableObject, IAddPageViewModel
    {
        private ShortCode _activeShortCode;

        public ShortCode ActiveShortCode
        {
            get { return _activeShortCode; }
            set
            {
                if (_activeShortCode == value) return;
                _activeShortCode = value;
                NotifyPropertyChanged("ActiveShortCode");
            }
        }

        private string _pageTitle;

        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                if (_pageTitle == value) return;
                _pageTitle = value;
                NotifyPropertyChanged("PageTitle");
            }
        }

        public ObservableCollection<string> Errors { get; private set; }

        public AddPageViewModel()
        {
            Errors = new ObservableCollection<string>();
        }

    }
}
