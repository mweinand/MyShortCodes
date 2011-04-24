using System;
using MyShortCodes.Phone.Infrastructure;

namespace MyShortCodes.Phone.Domain
{
    public class ShortCode : BindableObject
    {
        private int _shortCodeId;
        public int ShortCodeId
        {
            get { return _shortCodeId; }
            set
            {
                if (_shortCodeId == value) return;
                _shortCodeId = value;
                NotifyPropertyChanged("ShortCodeId");
            }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code == value) return;
                _code = value;
                NotifyPropertyChanged("Code");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private DateTime _lastUsed;
        public DateTime LastUsed
        {
            get { return _lastUsed; }
            set
            {
                if (_lastUsed == value) return;
                _lastUsed = value;
                NotifyPropertyChanged("LastUsed");
            }
        }

        private int _timesUsed;
        public int TimesUsed
        {
            get { return _timesUsed; }
            set
            {
                if (_timesUsed == value) return;
                _timesUsed = value;
                NotifyPropertyChanged("TimesUsed");
            }
        }
    }
}