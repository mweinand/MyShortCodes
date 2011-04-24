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
using Microsoft.Phone.Tasks;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.Storage;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class SendSmsCommandHandler : ICommandHandler<SendSmsCommand>
    {
        private readonly IStorageManager _storageManager;

        public SendSmsCommandHandler(IStorageManager storageManager)
        {
            _storageManager = storageManager;
        }

        public void Handle(SendSmsCommand command)
        {
            if (command.ShortCode == null)
            {
                return;
            }

            command.ShortCode.LastUsed = DateTime.Now;
            command.ShortCode.TimesUsed++;
            _storageManager.SaveData();

            var message = new SmsComposeTask();
            message.To = command.ShortCode.Code;
            message.Show();
        }
    }
}
