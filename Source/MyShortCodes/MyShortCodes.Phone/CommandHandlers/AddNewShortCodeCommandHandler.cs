using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Navigation;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.Domain;
using MyShortCodes.Phone.Services;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Infrastructure.Threads;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class AddNewShortCodeCommandHandler : ICommandHandler<AddNewShortCodeCommand>
    {
        private readonly INavigationServiceWrapper _navigationService;
        private readonly IContainer _container;
        private readonly ITrialService _trialService;
        private readonly IApplicationState _applicationState;
        private readonly IDialogService _dialogService;
        private readonly IUIThreadInvoker _uiThreadInvoker;

        public AddNewShortCodeCommandHandler(INavigationServiceWrapper navigationService, IContainer container, ITrialService trialService, IApplicationState applicationState, IDialogService dialogService, IUIThreadInvoker uiThreadInvoker)
        {
            _navigationService = navigationService;
            _container = container;
            _trialService = trialService;
            _applicationState = applicationState;
            _dialogService = dialogService;
            _uiThreadInvoker = uiThreadInvoker;
        }

        public void Handle(AddNewShortCodeCommand command)
        {
            _uiThreadInvoker.Invoke(() =>
            {
                if (_trialService.IsTrial() && _applicationState.ShortCodes.Count > 0)
                {
                    var result = _dialogService.Confirm("The trial version is limited to storing 1 short code only.  Click Okay to buy the full version.");
                    if (result)
                    {
                        _trialService.SendUserToMarketplace();
                    }
                    return;
                }

                var addPageViewModel = _container.GetInstance<IAddPageViewModel>();

                addPageViewModel.ActiveShortCode = new ShortCode();
                addPageViewModel.Errors.Clear();
                addPageViewModel.PageTitle = "add code";

                _navigationService.Navigate("/Views/AddPage.xaml");
            });
        }
    }
}
