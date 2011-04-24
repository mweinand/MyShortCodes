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
        private INavigationServiceWrapper _navigationService;
        private IContainer _container;
        private ITrialService _trialService;
        private IApplicationState _applicationState;
        private IDialogService _dialogService;

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

            _navigationService.Navigate("/AddPage.xaml");
        }
    }
}
