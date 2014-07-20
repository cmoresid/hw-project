using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyABC.WebUI.Localization
{
    public class LocalizedMessageService : ILocalizedMessageService
    {
        private static System.Resources.ResourceManager _instance;

        public LocalizedMessageService()
        {
        }

        private static System.Resources.ResourceManager Instance
        {
            get
            {
                if (_instance == null)
                    // TODO: Properly resolve resource file.
                    _instance = new System.Resources.ResourceManager("Messages", typeof(CompanyABC.WebUI.MvcApplication).Assembly);

                return _instance;
            }
        }

        // TODO: Localization tools were being stupid (or maybe it's just me)
        // so I hard coded values in. Fix me!
        public string ProductSaved
        {
            //get { return Instance.GetString("ProductSaved"); }
            get { return "{0} was saved."; }
        }

        public string ProductDeleted
        {
            //get { return Instance.GetString("ProductDeleted"); }
            get { return "{0} was deleted."; }
        }
    }
}