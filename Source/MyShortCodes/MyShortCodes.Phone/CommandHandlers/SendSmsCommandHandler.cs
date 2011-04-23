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

namespace MyShortCodes.Phone.CommandHandlers
{
    public class SendSmsCommandHandler : ICommandHandler<SendSmsCommand>
    {
        public void Handle(SendSmsCommand command)
        {
            var message = new SmsComposeTask();
            message.To = command.ShortCode.Code;
            message.Show();
        }
    }
}
