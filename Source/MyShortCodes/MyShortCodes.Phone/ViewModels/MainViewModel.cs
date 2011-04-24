using System.Collections.ObjectModel;
using MyShortCodes.Phone.Infrastructure;
using MyShortCodes.Phone.Domain;

namespace MyShortCodes.Phone.ViewModels
{
    public interface IMainViewModel
    {
        ObservableCollection<ShortCode> AllShortCodes { get; }
        ObservableCollection<ShortCode> RecentShortCodes { get; }
        ObservableCollection<ShortCode> MostUsedShortCodes { get; }
    }

    public class MainViewModel : BindableObject, IMainViewModel
    {
        public MainViewModel()
        {
            AllShortCodes = new ObservableCollection<ShortCode>();
            RecentShortCodes = new ObservableCollection<ShortCode>();
            MostUsedShortCodes = new ObservableCollection<ShortCode>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ShortCode> AllShortCodes { get; private set; }

        public ObservableCollection<ShortCode> RecentShortCodes { get; private set; }

        public ObservableCollection<ShortCode> MostUsedShortCodes { get; private set; }

    }
}