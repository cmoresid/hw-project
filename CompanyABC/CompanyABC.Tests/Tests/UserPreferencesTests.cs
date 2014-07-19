using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyABC.WebUI.Preferences;

namespace CompanyABC.Tests
{
    [TestClass]
    public class UserPreferencesTests
    {
        [TestMethod]
        public void BasicSetCookiePreferences_Test()
        {
            ConfigureHttpContext();

            UserPreferenceInfo info = new UserPreferenceInfo()
            {
                ProductsPerPage = 10,
                ProductColumnsToDisplay = new List<string>() { "ABCID", "Title" }
            };

            IUserPreferenceService userPreferences = new CookieUserPreferenceService(info);

            Assert.AreEqual(1, HttpContext.Current.Response.Cookies.Count);

            HttpCookie userPreferenceCookie = HttpContext.Current.Response.Cookies[0];
            
            Assert.IsNotNull(userPreferenceCookie);
            Assert.IsNotNull(userPreferenceCookie["ProductsPerPage"]);
            Assert.IsNotNull(userPreferenceCookie["ProductColumnsToDisplay"]);
            Assert.AreEqual("10", userPreferenceCookie["ProductsPerPage"]);
            Assert.AreEqual("ABCID,Title", userPreferenceCookie["ProductColumnsToDisplay"]);

            CleanupHttpContext();
        }

        [TestMethod]
        public void BasicGetCookiePreferences_Test()
        {
            ConfigureHttpContext();

            HttpCookie userPrefCookie = new HttpCookie("CompanyABCPreference");

            userPrefCookie["ProductsPerPage"] = "15";
            userPrefCookie["ProductColumnsToDisplay"] = "ABCID,Title";

            HttpContext.Current.Request.Cookies.Add(userPrefCookie);

            IUserPreferenceService userPreferences = new CookieUserPreferenceService();
            UserPreferenceInfo info = userPreferences.Preferences;

            Assert.AreEqual(15, info.ProductsPerPage);
            Assert.AreEqual(2, info.ProductColumnsToDisplay.Count());

            CleanupHttpContext();
        }

        private void ConfigureHttpContext()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest(null, "http://tempuri.org", null),
                new HttpResponse(null));
        }

        private void CleanupHttpContext()
        {
            HttpContext.Current = null;
        }
    }
}
