using System;
using System.Linq;
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
            var shortCodes = _applicationState.ShortCodes.OrderBy(c => c.Name);
            foreach (var shortCode in shortCodes)
            {
                viewModel.AllShortCodes.Add(shortCode);
            }

            viewModel.RecentShortCodes.Clear();
            // needs to be decending as the add process flips it
            var recentShortCodes = _applicationState.ShortCodes.OrderByDescending(c => c.LastUsed);
            foreach (var shortCode in recentShortCodes)
            {
                viewModel.RecentShortCodes.Add(shortCode);
            }
        }
    }
}