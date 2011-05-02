using Mangifera.Container;
using Mangifera.Messaging;
using Mangifera.Navigation;
using Mangifera.Threading;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.Domain;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class EditShortCodeCommandHandler : ICommandHandler<EditShortCodeCommand>
    {
        private readonly INavigationServiceWrapper _navigationService;
        private readonly IContainer _container;
        private readonly IUIThreadInvoker _uiThreadInvoker;

        public EditShortCodeCommandHandler(INavigationServiceWrapper navigationService, IContainer container, IUIThreadInvoker uiThreadInvoker)
        {
            _navigationService = navigationService;
            _container = container;
            _uiThreadInvoker = uiThreadInvoker;
        }

        public void Handle(EditShortCodeCommand command)
        {
            if (command.ShortCode == null)
            {
                return;
            }

            _uiThreadInvoker.Invoke(() =>
            {
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
            });
        }
    }
}
