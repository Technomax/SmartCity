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
    public class DualAxesLineAndColumnController : Controller
    {
        public ActionResult Index()
        {
            return View(new SmartCity.Models.BaseLineModel
            {
                highChart = DualAxesLineAndColumnSingleStation("Kathmandu", CustomChartsData.CategoryMonth,
                                      new Data(DualAxesLineAndColumn.GetDataForDualAxesLineAndColumnChart("Kathmandu", DateTime.Now.AddYears(-1), DateTime.Now, "Monthly", "AvgRainFall")),
                                      new Data(DualAxesLineAndColumn.GetDataForDualAxesLineAndColumnChart("Kathmandu", DateTime.Now.AddYears(-1), DateTime.Now, "Monthly", "AvgTemp"))),
                searchParam = new SmartCity.Models.Global.BaseLineSearchModel
                {
                    FromDate = DateTime.Now.AddYears(-1),
                    ToDate = DateTime.Now,
                    ddlStation = PairRepo.GetPairModel("Station"),
                    ddlMeasureOption = PairRepo.GetPairModel("BaseLine_CompareType"),
                    StationList = "Kathmandu"
                }
            });
        }

        public ActionResult _Chart(DateTime paramFromDate, DateTime paramToDate, string station, string displayOption)
        {
            return PartialView(new SmartCity.Models.BaseLineModel
            {
                highChart = DualAxesLineAndColumnSingleStation(station,
                displayOption == "Yearly" ? CustomChartsData.CategoryYear : displayOption == "Weekly" ? CustomChartsData.CategoryWeek : CustomChartsData.CategoryMonth,
                 new Data(DualAxesLineAndColumn.GetDataForDualAxesLineAndColumnChart(station, paramFromDate, paramToDate, displayOption, "AvgRainFall")),
                 new Data(DualAxesLineAndColumn.GetDataForDualAxesLineAndColumnChart(station, paramFromDate, paramToDate, displayOption, "AvgTemp")))
            });
        }

        private Highcharts DualAxesLineAndColumnSingleStation(string stationName, string[] categories, Data rainFall, Data temperature)
        {
            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { ZoomType = ZoomTypes.Xy })
                .SetTitle(new Title { Text = "Average Monthly Temperature and Rainfall in " + stationName })
                .SetSubtitle(new Subtitle { Text = "Source: Dummy" })
                .SetXAxis(new XAxis { Categories = categories })
                .SetYAxis(new[]
                          {
                              new YAxis
                              {
                                  Labels = new YAxisLabels
                                           {
                                               Formatter = "function() { return this.value +'°C'; }",
                                               Style = "color: '#89A54E'"
                                           },
                                  Title = new XAxisTitle
                                          {
                                              Text = "Temperature",
                                              Style = "color: '#89A54E'"
                                          }
                              },
                              new YAxis
                              {
                                  Labels = new YAxisLabels
                                           {
                                               Formatter = "function() { return this.value +' mm'; }",
                                               Style = "color: '#4572A7'"
                                           },
                                  Title = new XAxisTitle
                                          {
                                              Text = "Rainfall",
                                              Style = "color: '#4572A7'"
                                          },
                                  Opposite = true
                              }
                          })
                .SetTooltip(new Tooltip
                {
                    Formatter = "function() { return ''+ this.x +': '+ this.y + (this.series.name == 'Rainfall' ? ' mm' : '°C'); }"
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Left,
                    X = 120,
                    VerticalAlign = VerticalAligns.Top,
                    Y = 100,
                    Floating = true,
                    BackgroundColor = ColorTranslator.FromHtml("#FFFFFF")
                })
                .SetSeries(new[]
                           {
                               new Series
                               {
                                   Name = "Rainfall",
                                   Color = ColorTranslator.FromHtml("#4572A7"),
                                   Type = ChartTypes.Column,
                                   YAxis = 1,
                                   Data = rainFall
                               },
                               new Series
                               {
                                   Name = "Temperature",
                                   Color = ColorTranslator.FromHtml("#89A54E"),
                                   Type = ChartTypes.Spline,
                                   Data = temperature
                               }
                           });
            return chart;
        }
    }
}
