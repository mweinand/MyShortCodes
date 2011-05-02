using System;
using System.Linq;
using Mangifera.Messaging;
using Mangifera.Phone.UI;
using Mangifera.Threading;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Storage;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class DeleteShortCodeCommandHandler : ICommandHandler<DeleteShortCodeCommand>
    {
        private readonly IDialogService _dialogService;
        private readonly IApplicationState _applicationState;
        private readonly IStorageManager _storageManager;
        private readonly IUIThreadInvoker _uiThreadInvoker;

        public DeleteShortCodeCommandHandler(IDialogService dialogService, IApplicationState applicationState, IStorageManager storageManager, IUIThreadInvoker uiThreadInvoker)
        {
            _dialogService = dialogService;
            _applicationState = applicationState;
            _storageManager = storageManager;
            _uiThreadInvoker = uiThreadInvoker;
        }

        public void Handle(DeleteShortCodeCommand command)
        {
            if (command.ShortCode == null)
            {
                return;
            }

            _uiThreadInvoker.Invoke(() =>
            {
                if (!_dialogService.Confirm(String.Format("Are you sure you want to delete {0}?", command.ShortCode.Code)))
                {
                    return;
                }

                var existingCode = _applicationState.ShortCodes.SingleOrDefault(s => s.ShortCodeId == command.ShortCode.ShortCodeId);
                _applicationState.ShortCodes.Remove(existingCode);
                _storageManager.SaveData();
            });
        }
    }
}
