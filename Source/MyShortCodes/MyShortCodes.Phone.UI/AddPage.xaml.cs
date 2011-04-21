using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using MyShortCodes.Phone.UI.Storage;
using MyShortCodes.Phone.UI.ViewModels;
using MyShortCodes.Phone.Domain;
using MyShortCodes.Phone.State;

namespace MyShortCodes.Phone.UI
{
    public partial class AddPage : PhoneApplicationPage
    {
        private IStorageManager _storageManager;
        private IApplicationState _applicationState;

        public AddPage()
        {
            InitializeComponent();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            var shortCode = new ShortCode {Name = ShortCodeName.Text, Code = ShortCodeCode.Text};
            _applicationState.ShortCodes.Add(shortCode);
            _storageManager.SaveData();
            var mainUri = new Uri("/MainPage.xaml", UriKind.Relative);
            NavigationService.Navigate(mainUri);
        }
    }
}