using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartCity.Repository;
using SmartCity.Models.Global;

namespace SmartCity.Controllers
{
    public class HistoryTabularController : Controller
    {
        public ActionResult Index()
        {
            return View(new SmartCity.Models.TabularModel_Extended
            {
                tabularModel = TabularRepo.GetTabularReport(
                                     "Kathmandu", DateTime.Now.AddYears(-1), DateTime.Now, "Monthly"),
                searchParam = new SmartCity.Models.Global.BaseLineSearchModel
                {
                    FromDate = DateTime.Now.AddYears(-1),
                    ToDate = DateTime.Now,
                    ddlChartOption = PairRepo.GetPairModel("BaseLine_Option_Tabular"),
                    ddlMeasureOption = PairRepo.GetPairModel("BaseLine_CompareType"),
                    ddlStation = PairRepo.GetPairModel("Station"),
                    StationList = ((PairModel)PairRepo.GetPairModel("Station").FirstOrDefault()).Value.ToString(),
                    ddlWeatherParameter = PairRepo.GetPairModel("WeatherParameter")
                }
            });
        }

        public ActionResult _List(DateTime paramFromDate, DateTime paramToDate, string station, string displayOption, string chartOption, string tempParam)
        {
                string[] stationList = station.Split(',');
                SmartCity.Models.TabularModel_Extended tabularExtended = new Models.TabularModel_Extended();
                foreach (string stationName in stationList)
                {
                    if(tabularExtended.tabularModel==null)
                        tabularExtended.tabularModel=TabularRepo.GetTabularReport(stationName, paramFromDate, paramToDate, displayOption);
                    else
                    tabularExtended.tabularModel=
                    (from c in tabularExtended.tabularModel select c).Union(TabularRepo.GetTabularReport(stationName, paramFromDate, paramToDate, displayOption));
                }
                return PartialView(tabularExtended);
        }
    }
}
