using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyABC.Domain.Constants;

namespace CompanyABC.WebUI.Helpers
{
    public static class StatusHelper
    {
        public static MvcHtmlString ProductStatusHelper(this HtmlHelper html, string input)
        {
            string statusFormatStr = "<span style=\"{0}\">{1}</span>";

            switch (input)
            {
                case StatusCode.IN_STOCK:
                    return new MvcHtmlString(string.Format(statusFormatStr, "color: ForestGreen", input));
                case StatusCode.ON_THE_WAY:
                    return new MvcHtmlString(string.Format(statusFormatStr, "color: Orange", input));
                case StatusCode.OUT_OF_STOCK:
                    return new MvcHtmlString(string.Format(statusFormatStr, "color: Red; font-weight: bold;", input));
            }

            return new MvcHtmlString(input);
        }
    }
}