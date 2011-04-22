using System.Linq;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Domain;

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

        public StorageManager(IApplicationState applicationState, ISettingsManager settingsManager)
        {
            _applicationState = applicationState;
            _settingsManager = settingsManager;
        }

        public void SaveData()
        {
            _settingsManager.Put("ShortCodes", _applicationState.ShortCodes.ToArray());
            _settingsManager.Save();
        }

        public void LoadData()
        {
            _applicationState.ShortCodes.Clear();
            //_applicationState.IsDataLoaded = true;
            
            var shortCodes = _settingsManager.Get<ShortCode[]>("ShortCodes");                
            if(shortCodes == null)
            {
                return;
            }

            foreach(var shortCode in shortCodes)
            {
                _applicationState.ShortCodes.Add(shortCode);
            }
            _applicationState.IsDataLoaded = true;
        }
    }
}