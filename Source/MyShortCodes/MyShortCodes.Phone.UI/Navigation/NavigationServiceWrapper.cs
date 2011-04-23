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
using MyShortCodes.Phone.Navigation;
using Microsoft.Phone.Shell;

namespace MyShortCodes.Phone.UI.Navigation
{
    public class NavigationServiceWrapper : INavigationServiceWrapper
    {
        public void Navigate(string uriString)
        {
            var uri = new Uri(uriString, UriKind.Relative);
            var frame = (App.Current as App).RootFrame.Navigate(uri);
        }
    }
}
