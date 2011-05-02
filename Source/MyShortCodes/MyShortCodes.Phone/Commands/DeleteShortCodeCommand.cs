using Mangifera.Messaging;
using MyShortCodes.Phone.Domain;

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
