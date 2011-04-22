using System;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Storage;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class ApplicationLoadedCommandHandler : ICommandHandler<ApplicationLoadedCommand>
    {
        private readonly IApplicationState _applicationState;
        private readonly IStorageManager _storageManager;

        public ApplicationLoadedCommandHandler(IApplicationState applicationState, IStorageManager storageManager)
        {
            _applicationState = applicationState;
            _storageManager = storageManager;
        }

        public void Handle(ApplicationLoadedCommand command)
        {
            if(!_applicationState.IsDataLoaded)
            {
                _storageManager.LoadData();
            }
        }
    }
}