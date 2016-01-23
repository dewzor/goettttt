using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Globalization;
using System.Threading;

namespace DateSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }


        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            try
            {
                var languageCookie = HttpContext.Current.Request.Cookies["lang"];
                var userLanguages = HttpContext.Current.Request.UserLanguages;
                var cultureInfo = new CultureInfo(languageCookie != null
                    ? languageCookie.Value
                    : userLanguages != null
                    ? userLanguages[0]
                    : "en"
                );
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
