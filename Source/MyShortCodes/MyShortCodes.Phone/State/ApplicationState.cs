using System;
using MyShortCodes.Phone.Domain;
using System.Collections.Generic;

namespace MyShortCodes.Phone.State
{
    public interface IApplicationState
    {
        IList<ShortCode> ShortCodes { get; }
        bool IsDataLoaded { get; set; }
    }
    public class ApplicationState : IApplicationState
    {
        public IList<ShortCode> ShortCodes { get; private set; }
        public bool IsDataLoaded
        {
            get;
            set;
        }

        public ApplicationState()
        {
            ShortCodes = new List<ShortCode>();
        }
    }
}
