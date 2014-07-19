using System;
using CompanyABC.Domain.Repositories;
using CompanyABC.WebUI.Localization;
using CompanyABC.WebUI.Preferences;

namespace CompanyABC.WebUI.Container
{
    public class ProductControllerContainer : IProductControllerContainer
    {
        public ProductControllerContainer(IProductRepository productRepo, ILocalizedMessageService messageService, IUserPreferenceService userPrefService)
        {
            ProductRepository = productRepo;
            LocalizationMessageService = messageService;
            UserPreferenceService = userPrefService;
        }

        public IProductRepository ProductRepository { get; private set; }
        public ILocalizedMessageService LocalizationMessageService { get; private set; }
        public IUserPreferenceService UserPreferenceService { get; private set; }
    }
}