using System;
using Microsoft.Phone.Tasks;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.Storage;
using MyShortCodes.Phone.State;
using System.Linq;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class SendSmsCommandHandler : ICommandHandler<SendSmsCommand>
    {
        private readonly IStorageManager _storageManager;
        private readonly IApplicationState _applicationState;

        public SendSmsCommandHandler(IStorageManager storageManager, IApplicationState applicationState)
        {
            _storageManager = storageManager;
            _applicationState = applicationState;
        }

        public void Handle(SendSmsCommand command)
        {
            if (command.ShortCode == null)
            {
                return;
            }

            var shortCode = _applicationState.ShortCodes.SingleOrDefault(s => s.ShortCodeId == command.ShortCode.ShortCodeId);
            shortCode.LastUsed = DateTime.Now;
            shortCode.TimesUsed++;
            _storageManager.SaveData();

            var message = new SmsComposeTask {To = command.ShortCode.Code};
            message.Show();
        }
    }
}
