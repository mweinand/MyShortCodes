using System;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.Domain;

namespace MyShortCodes.Phone.Commands
{
    public class SendSmsCommand : ICommand
    {
        public ShortCode ShortCode { get; private set; }

        public SendSmsCommand(ShortCode code)
        {
            ShortCode = code;
        }
    }
}
