﻿using System.Windows;
using Mangifera.Container;
using Mangifera.Messaging;
using Microsoft.Phone.Controls;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.Commands;

namespace MyShortCodes.Phone.UI.Views
{
    public partial class AddPage : PhoneApplicationPage
    {
        public AddPage()
        {
            InitializeComponent();
            DataContext = MicroMap.GetInstance<IAddPageViewModel>();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            var commandBus = MicroMap.GetInstance<ICommandBus>();
            commandBus.PublishCommand(new SaveShortCodeCommand());
        }
    }
}