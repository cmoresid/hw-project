using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompanyABC.Domain.Repositories;
using CompanyABC.WebUI.Localization;
using CompanyABC.WebUI.Preferences;

namespace CompanyABC.WebUI.Container
{
    public interface IProductControllerContainer
    {
        IProductRepository ProductRepository { get; }
        ILocalizedMessageService LocalizationMessageService { get; }
        IUserPreferenceService UserPreferenceService { get; }
    }
}