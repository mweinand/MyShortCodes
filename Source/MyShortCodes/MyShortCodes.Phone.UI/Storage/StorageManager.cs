
using System.IO.IsolatedStorage;
using System.Linq;
using MyShortCodes.Phone.UI.ViewModels;

namespace MyShortCodes.Phone.UI.Storage
{
    public class StorageManager
    {
        public static void SaveData()
        {
            if(!IsolatedStorageSettings.ApplicationSettings.Contains("ShortCodes"))
            {
                IsolatedStorageSettings.ApplicationSettings.Add("ShortCodes", App.ViewModel.AllShortCodes.ToArray());
            } else
            {
                IsolatedStorageSettings.ApplicationSettings["ShortCodes"] = App.ViewModel.AllShortCodes.ToArray();
            }

            IsolatedStorageSettings.ApplicationSettings.Save();

        }

        public static void LoadData()
        {
            App.ViewModel.AllShortCodes.Clear();
            App.ViewModel.IsDataLoaded = true;
            if (!IsolatedStorageSettings.ApplicationSettings.Contains("ShortCodes")) return;
            
            var shortCodes = IsolatedStorageSettings.ApplicationSettings["ShortCodes"] as ShortCodeModel[];
                
            if(shortCodes == null)
            {
                return;
            }

            foreach(var shortCode in shortCodes)
            {
                App.ViewModel.AllShortCodes.Add(shortCode);
            }
        }
    }
}