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
using MyShortCodes.Phone.UI.ViewModels;

namespace MyShortCodes.Phone.UI
{
    public partial class AddPage : PhoneApplicationPage
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            var shortCode = new ShortCodeModel {Name = ShortCodeName.Text, Code = ShortCodeCode.Text};
            App.ViewModel.AllShortCodes.Add(shortCode);
            var mainUri = new Uri("/MainPage.xaml", UriKind.Relative);
            NavigationService.Navigate(mainUri);
        }
    }
}