using System;
using System.Windows;
using MyShortCodes.Phone.Navigation;

namespace MyShortCodes.Phone.UI.Navigation
{
    public class NavigationServiceWrapper : INavigationServiceWrapper
    {
        public void Navigate(string uriString)
        {
            var uri = new Uri(uriString, UriKind.Relative);
            ((App) Application.Current).RootFrame.Navigate(uri);
        }
    }
}
