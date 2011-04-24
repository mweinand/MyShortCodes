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
using System.Linq;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Storage;
using MyShortCodes.Phone.Navigation;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.Domain;
using MyShortCodes.Phone.Validation;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class SaveShortCodeCommandHandler : ICommandHandler<SaveShortCodeCommand>
    {
        private IApplicationState _applicationState;
        private IStorageManager _storageManager;
        private INavigationServiceWrapper _navigationService;
        private IContainer _container;
        private IValidator<ShortCode> _shortCodeValidator;

        public SaveShortCodeCommandHandler(IApplicationState applicationState, IStorageManager storageManager, INavigationServiceWrapper navigationService, IContainer container, IValidator<ShortCode> shortCodeValidator)
        {
            _applicationState = applicationState;
            _storageManager = storageManager;
            _navigationService = navigationService;
            _container = container;
            _shortCodeValidator = shortCodeValidator;
        }

        public void Handle(SaveShortCodeCommand command)
        {
            var addPageViewModel = _container.GetInstance<IAddPageViewModel>();
            
            // let's validate
            var result = _shortCodeValidator.Validate(addPageViewModel.ActiveShortCode);
            if (!result.IsValid)
            {
                addPageViewModel.Errors.Clear();
                foreach (var error in result.Errors)
                {
                    addPageViewModel.Errors.Add(error);
                }
                return;
            }

            // figure out editing or saving
            var existingShortCode = _applicationState.ShortCodes.SingleOrDefault(c => c.ShortCodeId == addPageViewModel.ActiveShortCode.ShortCodeId);
            if (existingShortCode == null)
            {
                addPageViewModel.ActiveShortCode.ShortCodeId = _applicationState.NextShortCodeId++;
                _applicationState.ShortCodes.Add(addPageViewModel.ActiveShortCode);
            } else {
                existingShortCode.Name = addPageViewModel.ActiveShortCode.Name;
                existingShortCode.Code = addPageViewModel.ActiveShortCode.Code;
            }
            
            // save to storage
            _storageManager.SaveData();

            _navigationService.Navigate("/MainPage.xaml");
        }
    }
}
