﻿using System;
using System.Linq;
using Mangifera.Container;
using Mangifera.Messaging;
using Mangifera.Navigation;
using Mangifera.Threading;
using Mangifera.Validation;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Storage;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.Domain;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class SaveShortCodeCommandHandler : ICommandHandler<SaveShortCodeCommand>
    {
        private readonly IApplicationState _applicationState;
        private readonly IStorageManager _storageManager;
        private readonly INavigationServiceWrapper _navigationService;
        private readonly IContainer _container;
        private readonly IValidator<ShortCode> _shortCodeValidator;
        private readonly IUIThreadInvoker _uiThreadInvoker;


        public SaveShortCodeCommandHandler(IApplicationState applicationState, IStorageManager storageManager, INavigationServiceWrapper navigationService, IContainer container, IValidator<ShortCode> shortCodeValidator, IUIThreadInvoker uiThreadInvoker)
        {
            _applicationState = applicationState;
            _storageManager = storageManager;
            _navigationService = navigationService;
            _container = container;
            _shortCodeValidator = shortCodeValidator;
            _uiThreadInvoker = uiThreadInvoker;
        }

        public void Handle(SaveShortCodeCommand command)
        {
            var addPageViewModel = _container.GetInstance<IAddPageViewModel>();
            
            // let's validate
            var result = _shortCodeValidator.Validate(addPageViewModel.ActiveShortCode);
            if (!result.IsValid)
            {
                _uiThreadInvoker.Invoke(() =>
                                            {
                                                addPageViewModel.Errors.Clear();
                                                foreach (var error in result.Errors)
                                                {
                                                    addPageViewModel.Errors.Add(error);
                                                }
                                            });
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

            _uiThreadInvoker.Invoke(() => _navigationService.Navigate("/Views/MainPage.xaml"));
        }
    }
}
