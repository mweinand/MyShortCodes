using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Navigation;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.Domain;
using MyShortCodes.Phone.Services;
using MyShortCodes.Phone.State;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class AddNewShortCodeCommandHandler : ICommandHandler<AddNewShortCodeCommand>
    {
        private readonly INavigationServiceWrapper _navigationService;
        private readonly IContainer _container;
        private readonly ITrialService _trialService;
        private readonly IApplicationState _applicationState;
        private readonly IDialogService _dialogService;

        public AddNewShortCodeCommandHandler(INavigationServiceWrapper navigationService, IContainer container, ITrialService trialService, IApplicationState applicationState, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _container = container;
            _trialService = trialService;
            _applicationState = applicationState;
            _dialogService = dialogService;
        }

        public void Handle(AddNewShortCodeCommand command)
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
        }
    }
}
