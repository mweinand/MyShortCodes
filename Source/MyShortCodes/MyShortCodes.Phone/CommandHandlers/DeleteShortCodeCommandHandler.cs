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
using MyShortCodes.Phone.Services;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Storage;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class DeleteShortCodeCommandHandler : ICommandHandler<DeleteShortCodeCommand>
    {
        private IDialogService _dialogService;
        private IApplicationState _applicationState;
        private IStorageManager _storageManager;
        private ICommandBus _commandBus;

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
