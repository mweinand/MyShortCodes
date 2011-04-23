﻿using System;
using System.Windows;
using Microsoft.Phone.Controls;
using MyShortCodes.Phone.Storage;
using MyShortCodes.Phone.Domain;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.Commands;

namespace MyShortCodes.Phone.UI
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