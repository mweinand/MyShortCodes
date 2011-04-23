using System;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.Domain;

namespace MyShortCodes.Phone.Commands
{
    public class EditShortCodeCommand : ICommand
    {
        private readonly ShortCode _shortCode;

        public EditShortCodeCommand(ShortCode shortCode)
        {
            // TODO: Complete member initialization
            _shortCode = shortCode;
        }

        public ShortCode ShortCode { get { return _shortCode; } }
    }
}
