using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CompanyABC.WebUI.Preferences
{
    public class CookieUserPreferenceService : IUserPreferenceService
    {
        private static readonly string COOKIE_NAME = "CompanyABCPreference";

        public CookieUserPreferenceService()
        {
        }

        public CookieUserPreferenceService(UserPreferenceInfo info)
        {
            this.Preferences = info;
        }

        public UserPreferenceInfo Preferences
        {
            get
            {
                return GetPreferencesFromCookie();
            }
            set
            {
                SetPreferenceCookie(value);
            }
        }

        private void SetPreferenceCookie(UserPreferenceInfo newUserPreferences)
        {
            HttpCookie userPrefCookie =
                HttpContext.Current.Request.Cookies[COOKIE_NAME]
                ?? new HttpCookie(COOKIE_NAME);

            userPrefCookie["ProductsPerPage"] = newUserPreferences.ProductsPerPage.ToString();
            userPrefCookie["ProductColumnsToDisplay"] = string.Join(",", newUserPreferences.ProductColumnsToDisplay.ToArray<string>());
            userPrefCookie.Expires = DateTime.Now.AddDays(365);

            HttpContext.Current.Response.Cookies.Add(userPrefCookie);
        }

        private UserPreferenceInfo GetPreferencesFromCookie()
        {
            HttpCookie userPrefCookie = HttpContext.Current.Request.Cookies[COOKIE_NAME];
            int productsPerPage = 10;
            List<string> productColumnsToDisplay = new List<string>();

            if (userPrefCookie == null)
            {
                UserPreferenceInfo newUserInfo = new UserPreferenceInfo() 
                {
                    ProductsPerPage = productsPerPage, 
                    ProductColumnsToDisplay = productColumnsToDisplay 
                };

                SetPreferenceCookie(newUserInfo);

                return newUserInfo;
            }

            if (userPrefCookie["ProductsPerPage"] != null)
            {
                if (!int.TryParse(userPrefCookie["ProductsPerPage"], out productsPerPage))
                {
                    productsPerPage = 10;
                }
            }

            if (userPrefCookie["ProductColumnsToDisplay"] != null)
            {
                productColumnsToDisplay = userPrefCookie["ProductColumnsToDisplay"].Split(new char[] { ',' }).ToList<string>();
            }

            return new UserPreferenceInfo()
            {
                ProductsPerPage = productsPerPage,
                ProductColumnsToDisplay = productColumnsToDisplay
            };
        }
    }
}