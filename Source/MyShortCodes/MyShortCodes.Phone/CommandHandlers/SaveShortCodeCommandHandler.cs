using System;
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
        private readonly IApplicationState _applicationState;
        private readonly IStorageManager _storageManager;
        private readonly INavigationServiceWrapper _navigationService;
        private readonly IContainer _container;
        private readonly IValidator<ShortCode> _shortCodeValidator;

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
                var nextShortCode = _applicationState.NextShortCodeId++;
                addPageViewModel.ActiveShortCode.ShortCodeId = nextShortCode;
                addPageViewModel.ActiveShortCode.LastUsed = DateTime.Now;
                _applicationState.ShortCodes.Add(addPageViewModel.ActiveShortCode);
            } else {
                existingShortCode.Name = addPageViewModel.ActiveShortCode.Name;
                existingShortCode.Code = addPageViewModel.ActiveShortCode.Code;
            }
            
            // save to storage
            _storageManager.SaveData();

            _navigationService.Navigate("/Views/MainPage.xaml");
        }
    }
}
