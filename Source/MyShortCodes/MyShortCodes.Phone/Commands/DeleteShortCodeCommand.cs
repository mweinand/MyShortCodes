using System;
using MyShortCodes.Phone.Domain;
using MyShortCodes.Phone.Infrastructure.Messaging;

namespace MyShortCodes.Phone.Commands
{
    public class DeleteShortCodeCommand : ICommand
    {
        private readonly ShortCode _shortCode;

        public DeleteShortCodeCommand(ShortCode shortCode)
        {
            _shortCode = shortCode;
        }

        public ShortCode ShortCode { get { return _shortCode; } }
    }
}
