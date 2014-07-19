using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyABC.WebUI.Preferences
{
    public interface IUserPreferenceService
    {
        UserPreferenceInfo Preferences { get; set; }
    }
}
