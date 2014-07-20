using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyABC.WebUI.Localization
{
    public interface ILocalizedMessageService
    {
        string ProductSaved { get; }
        string ProductDeleted { get; }
    }
}
