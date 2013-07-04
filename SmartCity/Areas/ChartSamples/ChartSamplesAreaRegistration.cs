
using System.Web.Mvc;

namespace SmartCity.Areas.ChartSamples
{
    public class ChartSamplesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ChartSamples";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ChartSamples_default",
                "ChartSamples/{controller}/{action}/{id}",
                new { action = "Index", controller="SampleBoard", id = UrlParameter.Optional }
            );
        }
    }
}
