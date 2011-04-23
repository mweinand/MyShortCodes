using System;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.State;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class MainPageLoadedCommandHandler : ICommandHandler<MainPageLoadedCommand>
    {
        private IContainer _container;
        private IApplicationState _applicationState;

        public MainPageLoadedCommandHandler(IContainer container, IApplicationState applicationState)
        {
            _container = container;
            _applicationState = applicationState;
        }

        public void Handle(MainPageLoadedCommand command)
        {
            var viewModel = _container.GetInstance<IMainViewModel>();

            viewModel.AllShortCodes.Clear();
            foreach (var shortCode in _applicationState.ShortCodes)
            {
                viewModel.AllShortCodes.Add(shortCode);
            }
        }
    }
}