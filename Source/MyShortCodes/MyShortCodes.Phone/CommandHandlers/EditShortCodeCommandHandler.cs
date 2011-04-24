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
        private INavigationServiceWrapper _navigationService;
        private IContainer _container;

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

            addPageViewModel.ActiveShortCode = new ShortCode() {
                ShortCodeId = command.ShortCode.ShortCodeId,
                Name = command.ShortCode.Name,
                Code = command.ShortCode.Code
            };
            addPageViewModel.Errors.Clear();
            addPageViewModel.PageTitle = "edit code";

            _navigationService.Navigate("/AddPage.xaml");
        }
    }
}
