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

namespace MyShortCodes.Phone.CommandHandlers
{
    public class AddNewShortCodeCommandHandler : ICommandHandler<AddNewShortCodeCommand>
    {
        private INavigationServiceWrapper _navigationService;
        private IContainer _container;

        public AddNewShortCodeCommandHandler(INavigationServiceWrapper navigationService, IContainer container)
        {
            _navigationService = navigationService;
            _container = container;
        }

        public void Handle(AddNewShortCodeCommand command)
        {
            var addPageViewModel = _container.GetInstance<IAddPageViewModel>();

            addPageViewModel.ActiveShortCode = new ShortCode();
            addPageViewModel.Errors.Clear();
            addPageViewModel.PageTitle = "add code";

            _navigationService.Navigate("/AddPage.xaml");
        }
    }
}
