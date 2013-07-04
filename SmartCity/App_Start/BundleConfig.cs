using System.Web;
using System.Web.Optimization;

namespace SmartCity
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
               "~/Scripts/core.js",
               "~/Scripts/jquery-1.9.1.js",
               "~/Scripts/jquery-ui-1.8.20.js",
               "~/Scripts/bootstrap.js",
               "~/Scripts/bootstrap-datepicker.js",
               "~/Scripts/jquery.unobtrusive-ajax.js",
               "~/Scripts/jquery.validate.unobtrusive.js",
               "~/Scripts/Highcharts-3.0.1/js/highcharts.js",
               "~/Scripts/Highcharts-3.0.1/js/highcharts.src.js",               
               "~/Scripts/prettify-small-1-Jun-2011/google-code-prettify/prettify.js",
               "~/Scripts/modernizr-*"
               
               ));
            
            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-custom.css",
                "~/Content/bootstrap-mvc-validation.css",
                "~/Content/datepicker.css",
                "~/Scripts/prettify-small-1-Jun-2011/google-code-prettify/prettify.css",
                "~/Content/themes/start/jquery-ui-1.8.16.custom.css"
                ));
        }
    }
}