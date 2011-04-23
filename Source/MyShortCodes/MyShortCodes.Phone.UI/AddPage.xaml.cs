using System;
using System.Windows;
using Microsoft.Phone.Controls;
using MyShortCodes.Phone.Storage;
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