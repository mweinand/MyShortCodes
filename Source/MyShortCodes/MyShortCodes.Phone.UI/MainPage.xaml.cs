using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.UI.ViewModels;
using MyShortCodes.Phone.Domain;

namespace MyShortCodes.Phone.UI
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = MicroMap.GetInstance<IMainViewModel>();
            Loaded += MainPage_Loaded;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var commandBus = MicroMap.GetInstance<ICommandBus>();
            commandBus.PublishCommand(new MainPageLoadedCommand());
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

            var shortCode = listBox.SelectedItem as ShortCode;
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