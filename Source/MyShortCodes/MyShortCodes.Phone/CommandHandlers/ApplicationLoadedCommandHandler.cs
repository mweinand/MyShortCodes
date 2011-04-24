using System;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Storage;
using MyShortCodes.Phone.Services;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class ApplicationLoadedCommandHandler : ICommandHandler<ApplicationLoadedCommand>
    {
        private readonly IApplicationState _applicationState;
        private readonly IStorageManager _storageManager;
        private readonly IDialogService _dialogService;

        public ApplicationLoadedCommandHandler(IApplicationState applicationState, IStorageManager storageManager, IDialogService dialogService)
        {
            _applicationState = applicationState;
            _storageManager = storageManager;
            _dialogService = dialogService;
        }

        public void Handle(ApplicationLoadedCommand command)
        {
            if(!_applicationState.IsDataLoaded)
            {
                _storageManager.LoadData();
            }

            _dialogService.Alert("By running this application you agree to not use it while driving and we are not responsible for what happens to you while running this application.");
        }
    }
}