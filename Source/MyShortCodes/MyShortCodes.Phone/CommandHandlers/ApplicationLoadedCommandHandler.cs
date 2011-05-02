using Mangifera.Messaging;
using Mangifera.Phone.UI;
using Mangifera.Threading;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Storage;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class ApplicationLoadedCommandHandler : ICommandHandler<ApplicationLoadedCommand>
    {
        private readonly IApplicationState _applicationState;
        private readonly IStorageManager _storageManager;
        private readonly IDialogService _dialogService;
        private readonly IUIThreadInvoker _uiThreadInvoker;

        public ApplicationLoadedCommandHandler(IApplicationState applicationState, IStorageManager storageManager, IDialogService dialogService, IUIThreadInvoker uiThreadInvoker)
        {
            _applicationState = applicationState;
            _storageManager = storageManager;
            _dialogService = dialogService;
            _uiThreadInvoker = uiThreadInvoker;
        }

        public void Handle(ApplicationLoadedCommand command)
        {
            if(!_applicationState.IsDataLoaded)
            {
                _applicationState.IsDataLoading = true;
                _storageManager.LoadData();
            }

            _uiThreadInvoker.Invoke(() => _dialogService.Alert("By running this application you agree to not use it while driving and we are not responsible for what happens to you while running this application."));
        }
    }
}