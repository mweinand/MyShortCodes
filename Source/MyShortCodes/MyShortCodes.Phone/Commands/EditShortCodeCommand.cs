using Mangifera.Messaging;
using MyShortCodes.Phone.Domain;

namespace MyShortCodes.Phone.Commands
{
    public class EditShortCodeCommand : ICommand
    {
        private readonly ShortCode _shortCode;

        public EditShortCodeCommand(ShortCode shortCode)
        {
            _shortCode = shortCode;
        }

        public ShortCode ShortCode { get { return _shortCode; } }
    }
}
