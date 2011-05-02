using System.Linq;
using Mangifera.Messaging;
using Mangifera.Phone;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Domain;
using MyShortCodes.Phone.Commands;

namespace MyShortCodes.Phone.Storage
{
    public interface IStorageManager
    {
        void SaveData();
        void LoadData();
    }

    public class StorageManager : IStorageManager
    {
        private readonly IApplicationState _applicationState;
        private readonly ISettingsManager _settingsManager;
        private readonly ICommandBus _commandBus;

        public StorageManager(IApplicationState applicationState, ISettingsManager settingsManager, ICommandBus commandBus)
        {
            _applicationState = applicationState;
            _settingsManager = settingsManager;
            _commandBus = commandBus;
        }

        public void SaveData()
        {
            _settingsManager.Put("NextShortCodeId", _applicationState.NextShortCodeId);
            _settingsManager.Put("ShortCodes", _applicationState.ShortCodes.ToArray());
            _settingsManager.Save();
            _commandBus.PublishCommand(new DataLoadedMessage());
        }

        public void LoadData()
        {
            _applicationState.ShortCodes.Clear();
            
            var shortCodes = _settingsManager.Get<ShortCode[]>("ShortCodes");
            var nextShortCodeId = _settingsManager.Get<int>("NextShortCodeId");
            
            _applicationState.NextShortCodeId = nextShortCodeId > 0 ? nextShortCodeId : 1;
            
            if (shortCodes != null)
            {
                foreach (var shortCode in shortCodes)
                {
                    _applicationState.ShortCodes.Add(shortCode);
                }
            }

            _applicationState.IsDataLoaded = true;
            _applicationState.IsDataLoading = false;

            _commandBus.PublishCommand(new DataLoadedMessage());
        }
    }
}