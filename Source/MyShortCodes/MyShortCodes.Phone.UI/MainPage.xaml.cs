using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.Domain;
using MyShortCodes.Phone.ViewModels;

namespace MyShortCodes.Phone.UI
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ICommandBus _commandBus;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _commandBus = MicroMap.GetInstance<ICommandBus>();

            // Set the data context of the listbox control to the sample data
            DataContext = MicroMap.GetInstance<IMainViewModel>();
            Loaded += MainPage_Loaded;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var commandBus = MicroMap.GetInstance<ICommandBus>();
            _commandBus.PublishCommand(new MainPageLoadedCommand());
        }

        private void ApplicationBarAddNewClick(object sender, EventArgs e)
        {
            _commandBus.PublishCommand(new AddNewShortCodeCommand());
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

            _commandBus.PublishCommand(new SendSmsCommand(shortCode));
        }

        private void EditItemClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem == null) { return; }

            var shortCode = menuItem.DataContext as ShortCode;
            if (shortCode == null) { return; }

            _commandBus.PublishCommand(new EditShortCodeCommand(shortCode));
        }

        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem == null) { return; }

            var shortCode = menuItem.DataContext as ShortCode;
            if (shortCode == null) { return; }

            _commandBus.PublishCommand(new DeleteShortCodeCommand(shortCode));
        }
    }
}