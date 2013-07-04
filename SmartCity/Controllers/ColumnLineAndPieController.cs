using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartCity.Repository;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using DotNet.Highcharts;
using SmartCity.Samples.Models;
using System.Drawing;
using Point = DotNet.Highcharts.Options.Point;
using SmartCity.Models.Global;

namespace SmartCity.Controllers
{
    public class ColumnLineAndPieController : Controller
    {
        public ActionResult Index()
        {
            return View(new SmartCity.Models.BaseLineModel
            {
                highChart = ColumnLineAndPie(SmartCity.Repository.ColumnLineAndPieRepo.GetCategoryArray("AirPollution, MinTemp, MaxTemp, RainFall"),
                SmartCity.Repository.ColumnLineAndPieRepo.GetSeriesArray("Kathmandu,Pokhara,Butwal,Bhairahawa", DateTime.Now.AddYears(-1), DateTime.Now)),
                searchParam = new SmartCity.Models.Global.BaseLineSearchModel
                {
                    FromDate = DateTime.Now.AddYears(-1),
                    ToDate = DateTime.Now,
                    ddlStation = PairRepo.GetPairModel("Station"),
                    StationList = "Kathmandu,Pokhara,Butwal,Bhairahawa"
                }
            });
        }

        public ActionResult _Chart(DateTime paramFromDate, DateTime paramToDate, string station)
        {
            return PartialView(new SmartCity.Models.BaseLineModel
            {
                highChart = ColumnLineAndPie(SmartCity.Repository.ColumnLineAndPieRepo.GetCategoryArray("AirPollution, MinTemp, MaxTemp, RainFall"),
                SmartCity.Repository.ColumnLineAndPieRepo.GetSeriesArray(station, paramFromDate, paramToDate))
            });
        }

        private Highcharts ColumnLineAndPie(string[] categories, Series[] series)
        {
            Highcharts chart = new Highcharts("chart")
                .SetTitle(new Title { Text = "Combination chart" })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
                .SetXAxis(new XAxis { Categories = categories })
                .SetTooltip(new Tooltip { Formatter = "TooltipFormatter" })
                .AddJavascripFunction("TooltipFormatter",
                                      @"var s;
                    if (this.point.name) { // the pie chart
                       s = ''+
                          this.point.name +': '+ this.y +' fruits';
                    } else {
                       s = ''+
                          this.x  +': '+ this.y;
                    }
                    return s;")
                .SetLabels(new Labels
                {
                    Items = new[]
                                       {
                                           new LabelsItems
                                           {
                                               Html = "Average Weather Status",
                                               Style = "left: '40px', top: '8px', color: 'black'"
                                           }
                                       }
                })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        Center = new[] { "100", "80" },
                        Size = "100",
                        ShowInLegend = false,
                        DataLabels = new PlotOptionsPieDataLabels { Enabled = false }
                    }
                })
                .SetSeries(series);
            return chart;
        }
    }
}
