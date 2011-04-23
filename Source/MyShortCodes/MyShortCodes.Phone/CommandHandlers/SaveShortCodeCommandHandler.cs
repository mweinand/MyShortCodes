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
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Storage;
using MyShortCodes.Phone.Navigation;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.ViewModels;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class SaveShortCodeCommandHandler : ICommandHandler<SaveShortCodeCommand>
    {
        private IApplicationState _applicationState;
        private IStorageManager _storageManager;
        private INavigationServiceWrapper _navigationService;
        private IContainer _container;

        public SaveShortCodeCommandHandler(IApplicationState applicationState, IStorageManager storageManager, INavigationServiceWrapper navigationService, IContainer container)
        {
            _applicationState = applicationState;
            _storageManager = storageManager;
            _navigationService = navigationService;
            _container = container;
        }

        public void Handle(SaveShortCodeCommand command)
        {
            var addPageViewModel = _container.GetInstance<IAddPageViewModel>();
            
            // figure out editing or saving
            if (!_applicationState.ShortCodes.Contains(addPageViewModel.ActiveShortCode))
            {
                _applicationState.ShortCodes.Add(addPageViewModel.ActiveShortCode);
            }
            
            _storageManager.SaveData();

            _navigationService.Navigate("/MainPage.xaml");
        }
    }
}
