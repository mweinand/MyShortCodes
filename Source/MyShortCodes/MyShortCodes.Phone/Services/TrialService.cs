using System;
using Microsoft.Phone.Marketplace;

namespace MyShortCodes.Phone.Services
{
    public interface ITrialService
    {
        bool IsTrial();
    }

    public class TrialService : ITrialService
    {
        public bool IsTrial()
        {
            var license = new LicenseInformation();
            return license.IsTrial();
        }
    }
}
