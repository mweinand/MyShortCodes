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
using Microsoft.Phone.Tasks;
using MyShortCodes.Phone.UI.Storage;
using MyShortCodes.Phone.UI.ViewModels;

namespace MyShortCodes.Phone.UI
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                StorageManager.LoadData();
            }
        }

        private void ApplicationBarAddNewClick(object sender, EventArgs e)
        {
            var destination = new Uri("/AddPage.xaml", UriKind.Relative);
            NavigationService.Navigate(destination);
        }

        private void CodeListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if(listBox == null)
            {
                return;
            }

            var shortCode = listBox.SelectedItem as ShortCodeModel;
            if(shortCode == null)
            {
                return;
            }

            var message = new SmsComposeTask();
            message.To = shortCode.Code;
            message.Show();
        }
    }
}