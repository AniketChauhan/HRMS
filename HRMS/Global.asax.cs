using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CaptchaMvc.Infrastructure;
using CaptchaMvc.Interface;
using CaptchaMvc.Models;

namespace HRMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //start
            var cap = (DefaultCaptchaManager)CaptchaUtils.CaptchaManager;
            cap.CharactersFactory = () => "my characters";
            cap.PlainCaptchaPairFactory = length =>
            {
                string randomtext = RandomText.Generate(cap.CharactersFactory(), length);
                bool ignorecase = false;
                return new KeyValuePair<string, ICaptchaValue>(Guid.NewGuid().ToString("N"),

                    new StringCaptchaValue(randomtext, randomtext, ignorecase)
                    );

            };
            //end


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
