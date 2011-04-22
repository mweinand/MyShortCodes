using System;
using MyShortCodes.Phone.Domain;
using System.Collections.Generic;

namespace MyShortCodes.Phone.State
{
    public interface IApplicationState
    {
        IList<ShortCode> ShortCodes { get; set; }
        bool IsDataLoaded { get; set; }
    }
    public class ApplicationState : IApplicationState
    {
        public IList<ShortCode> ShortCodes { get; set; }
        public bool IsDataLoaded
        {
            get;
            set;
        }
    }
}
