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
using System.IO.IsolatedStorage;

namespace MyShortCodes.Phone.Storage
{
    public interface ISettingsManager
    {
        TType Get<TType>(string name) where TType : class;
        void Put(string name, object data);
        void Save();
    }

    public class SettingsManager : ISettingsManager
    {
        public TType Get<TType>(string name) where TType : class
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(name))
            {
                return null;
            }
            var data = IsolatedStorageSettings.ApplicationSettings[name] as TType;
            return data;
        }

        public void Put(string name, object data) {

            if(!IsolatedStorageSettings.ApplicationSettings.Contains(name))
            {
                IsolatedStorageSettings.ApplicationSettings.Add(name, data);
            } else
            {
                IsolatedStorageSettings.ApplicationSettings[name] = data;
            }
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public void Save()
        {
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}
