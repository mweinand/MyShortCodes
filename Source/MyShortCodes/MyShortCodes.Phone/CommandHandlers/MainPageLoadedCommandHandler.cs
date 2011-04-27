using System.Linq;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Storage;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class MainPageLoadedCommandHandler : ICommandHandler<MainPageLoadedCommand>
    {
        private readonly IApplicationState _applicationState;
        private readonly IStorageManager _storageManager;

        public MainPageLoadedCommandHandler(IApplicationState applicationState, IStorageManager storageManager)
        {
            _applicationState = applicationState;
            _storageManager = storageManager;
        }

        public void Handle(MainPageLoadedCommand command)
        {
            if (!_applicationState.IsDataLoaded && !_applicationState.IsDataLoading)
            {
                _storageManager.LoadData();
            }
        }
    }
}