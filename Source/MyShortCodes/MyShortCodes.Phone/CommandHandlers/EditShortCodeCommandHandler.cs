using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.Navigation;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.Domain;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class EditShortCodeCommandHandler : ICommandHandler<EditShortCodeCommand>
    {
        private readonly INavigationServiceWrapper _navigationService;
        private readonly IContainer _container;

        public EditShortCodeCommandHandler(INavigationServiceWrapper navigationService, IContainer container)
        {
            _navigationService = navigationService;
            _container = container;
        }

        public void Handle(EditShortCodeCommand command)
        {
            if (command.ShortCode == null)
            {
                return;
            }

            var addPageViewModel = _container.GetInstance<IAddPageViewModel>();

            addPageViewModel.ActiveShortCode = new ShortCode
                                                   {
                                                       ShortCodeId = command.ShortCode.ShortCodeId,
                                                       Name = command.ShortCode.Name,
                                                       Code = command.ShortCode.Code
                                                   };
            addPageViewModel.Errors.Clear();
            addPageViewModel.PageTitle = "edit code";

            _navigationService.Navigate("/Views/AddPage.xaml");
        }
    }
}
