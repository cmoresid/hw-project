using System;
using System.Collections.Generic;

namespace CompanyABC.WebUI.Preferences
{
    public class UserPreferenceInfo
    {
        public int ProductsPerPage { get; set; }
        public IEnumerable<string> ProductColumnsToDisplay { get; set; }
    }
}