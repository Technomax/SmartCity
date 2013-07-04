using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartCity.Repository;
using SmartCity.Models.Global;

namespace SmartCity.Controllers
{
    public class ForecastTabularController : Controller
    {
        public ActionResult Index()
        {
            return View(new SmartCity.Models.TabularModel_Forecast
            {
                tabularModel = TabularRepo.GetTabularForecast("Kathmandu", DateTime.Now, 1),
                searchParam = new SmartCity.Models.Global.ForecastSearchModel
                {
                    SetDate = DateTime.Now,
                    ddlStation = PairRepo.GetPairModel("Station"),
                }
            });
        }

        public ActionResult _List(DateTime paramSetDate, string station, double airPollutionLevel)
        {
            //string[] stationList = station.Split(',');
            SmartCity.Models.TabularModel_Forecast tabularForecast = new Models.TabularModel_Forecast();
            //foreach (string stationName in stationList)
            //{
            if (tabularForecast.tabularModel == null)
                tabularForecast.tabularModel = TabularRepo.GetTabularForecast(station, paramSetDate, airPollutionLevel);
            else
                tabularForecast.tabularModel =
                (from c in tabularForecast.tabularModel select c).Union(TabularRepo.GetTabularForecast(station, paramSetDate, airPollutionLevel));
            //}
            return PartialView(tabularForecast);
        }
    }
}
