using System;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.Infrastructure.Threads;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.State;
using System.Linq;
using MyShortCodes.Phone.Domain;

namespace MyShortCodes.Phone.CommandHandlers
{
    public class DataLoadedCommandHandler : ICommandHandler<DataLoadedMessage>
    {
        private readonly IApplicationState _applicationState;
        private readonly IContainer _container;
        private readonly IUIThreadInvoker _uiThreadInvoker;

        public DataLoadedCommandHandler(IContainer container, IApplicationState applicationState, IUIThreadInvoker uiThreadInvoker)
        {
            _container = container;
            _applicationState = applicationState;
            _uiThreadInvoker = uiThreadInvoker;
        }

        public void Handle(DataLoadedMessage command)
        {
            // handle the lifting behind
            var shortCodes = _applicationState.ShortCodes.Select(c => new ShortCode
            {
                ShortCodeId = c.ShortCodeId,
                Name = c.Name,
                Code = c.Code,
                LastUsed = c.LastUsed,
                TimesUsed = c.TimesUsed
            });

            var allShortCodes = shortCodes.OrderBy(c => c.Name);
            var recentShortCodes = shortCodes.OrderByDescending(c => c.LastUsed).Take(5);
            var mostUsedShortCodes = shortCodes.Where(c => c.TimesUsed > 0).OrderByDescending(c => c.TimesUsed).Take(5);

            _uiThreadInvoker.Invoke(() =>
            {
                var viewModel = _container.GetInstance<IMainViewModel>();

                viewModel.AllShortCodes.Clear();
                foreach (var shortCode in allShortCodes)
                {
                    viewModel.AllShortCodes.Add(shortCode);
                }

                viewModel.RecentShortCodes.Clear();
                // needs to be decending as the add process flips it
                foreach (var shortCode in recentShortCodes)
                {
                    viewModel.RecentShortCodes.Add(shortCode);
                }

                viewModel.MostUsedShortCodes.Clear();
                // needs to be decending as the add process flips it
                foreach (var shortCode in mostUsedShortCodes)
                {
                    viewModel.MostUsedShortCodes.Add(shortCode);
                }
            });
        }
    }
}
