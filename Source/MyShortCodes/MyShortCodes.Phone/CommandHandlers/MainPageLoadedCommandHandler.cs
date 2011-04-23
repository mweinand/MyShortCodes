using System;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.ViewModels;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class MainPageLoadedCommandHandler : ICommandHandler<MainPageLoadedCommand>
    {
        public void Handle(MainPageLoadedCommand command)
        {
            var viewModel = MicroMap.GetInstance<IMainViewModel>();
        }
    }
}