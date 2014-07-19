using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyABC.WebUI.Localization
{
    public class LocalizedMessageService : ILocalizedMessageService
    {
        private static System.Resources.ResourceManager _instance;

        private LocalizedMessageService()
        {
        }

        private static System.Resources.ResourceManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new System.Resources.ResourceManager("Messages", typeof(CompanyABC.WebUI.MvcApplication).Assembly);

                return _instance;
            }
        }

        public string ProductSaved
        {
            get { return Instance.GetString("ProductSaved"); }
        }
    }
}