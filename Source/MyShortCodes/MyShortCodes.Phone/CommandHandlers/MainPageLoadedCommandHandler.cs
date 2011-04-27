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
        private readonly IContainer _container;
        private readonly IApplicationState _applicationState;

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
            var recentShortCodes = _applicationState.ShortCodes.OrderByDescending(c => c.LastUsed).Take(5);
            foreach (var shortCode in recentShortCodes)
            {
                viewModel.RecentShortCodes.Add(shortCode);
            }

            viewModel.MostUsedShortCodes.Clear();
            // needs to be decending as the add process flips it
            var mostUsedShortCodes = _applicationState.ShortCodes.Where(c => c.TimesUsed > 0).OrderByDescending(c => c.TimesUsed).Take(5);
            foreach (var shortCode in mostUsedShortCodes)
            {
                viewModel.MostUsedShortCodes.Add(shortCode);
            }
        }
    }
}