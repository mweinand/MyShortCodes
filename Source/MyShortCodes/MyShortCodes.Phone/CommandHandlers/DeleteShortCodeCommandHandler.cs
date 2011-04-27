using System;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.Services;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Storage;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class DeleteShortCodeCommandHandler : ICommandHandler<DeleteShortCodeCommand>
    {
        private readonly IDialogService _dialogService;
        private readonly IApplicationState _applicationState;
        private readonly IStorageManager _storageManager;
        private readonly ICommandBus _commandBus;

        public DeleteShortCodeCommandHandler(IDialogService dialogService, IApplicationState applicationState, IStorageManager storageManager, ICommandBus commandBus)
        {
            _dialogService = dialogService;
            _applicationState = applicationState;
            _storageManager = storageManager;
            _commandBus = commandBus;
        }

        public void Handle(DeleteShortCodeCommand command)
        {
            if (command.ShortCode == null)
            {
                return;
            }

            if (!_dialogService.Confirm(String.Format("Are you sure you want to delete {0}?", command.ShortCode.Code)))
            {
                return;
            }

            _applicationState.ShortCodes.Remove(command.ShortCode);
            _storageManager.SaveData();
            _commandBus.PublishCommand(new MainPageLoadedCommand());
        }
    }
}
