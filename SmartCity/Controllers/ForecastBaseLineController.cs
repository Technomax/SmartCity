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
    public class ForecastBaseLineController : Controller
    {
        public ActionResult Index()
        {
            var value = BaseLineRepo.GetDataForForecastBaseLineChart("Kathmandu", DateTime.Now, 0);
            return View(new SmartCity.Models.BaseLinePredictModel
            {

                highChart = BasicLineSingleStation(
                                      " MinTemp, MaxTemp, and RainFall",
                                      new Data(value[0]),
                                      new Data(value[1]),
                                      new Data(value[2]),
                                      CustomChartsData.CategoryDays
                                      ),
                searchParam = new SmartCity.Models.Global.ForecastSearchModel
                {
                    SetDate = DateTime.Now,
                    ddlStation = PairRepo.GetPairModel("Station"),
                }
            });
        }

        public ActionResult _Chart(DateTime paramSetDate, string station, double airPollutionLevel)
        {
            var value = BaseLineRepo.GetDataForForecastBaseLineChart(station, paramSetDate, airPollutionLevel);
            //if (chartOption == "Single Station")
            //{
                return PartialView(new SmartCity.Models.BaseLinePredictModel
                {
                    highChart = BasicLineSingleStation(
                                      " MinTemp, MaxTemp, and RainFall",
                                      new Data(value[0]),
                                      new Data(value[1]),
                                      new Data(value[2]),
                                      CustomChartsData.CategoryDays
                                      )
                });
           // }
            //else
            //{
            //    string[] stationList = station.Split(',');
            //    return PartialView(new SmartCity.Models.BaseLinePredictModel
            //    {
            //        highChart = BasicLineMultipleStation(
            //                              displayOption + " " + tempParam,
            //                              new PairModel { Key = stationList[0], Value = new Data(BaseLineRepo.GetDataForBaseLineChart(stationList[0], paramFromDate, paramToDate, displayOption, tempParam)) },
            //                              new PairModel { Key = stationList[1], Value = new Data(BaseLineRepo.GetDataForBaseLineChart(stationList[1], paramFromDate, paramToDate, displayOption, tempParam)) },
            //                              new PairModel { Key = stationList[2], Value = new Data(BaseLineRepo.GetDataForBaseLineChart(stationList[2], paramFromDate, paramToDate, displayOption, tempParam)) },
            //                              new PairModel { Key = stationList[3], Value = new Data(BaseLineRepo.GetDataForBaseLineChart(stationList[3], paramFromDate, paramToDate, displayOption, tempParam)) },
            //                              displayOption == "Yearly" ? CustomChartsData.CategoryYear : displayOption == "Weekly" ? CustomChartsData.CategoryWeek : CustomChartsData.CategoryMonth
            //                              )
            //    });
            //}
        }

        private Highcharts BasicLineSingleStation(string chartHeading,  Data minTemp, Data maxTemp, Data rainFall, string[] categories)
        {
            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart
                {
                    DefaultSeriesType = ChartTypes.Line,
                    MarginRight = 130,
                    MarginBottom = 25,
                    ClassName = "chart"
                })
                .SetTitle(new Title
                {
                    Text = chartHeading,
                    X = -20
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Source: Dummy Data",
                    X = -20
                })
                .SetXAxis(new XAxis { Categories = categories })
                .SetYAxis(new YAxis
                {
                    Title = new XAxisTitle { Text = "Temperature (°C)" },
                    PlotLines = new[]
                                          {
                                              new XAxisPlotLines
                                              {
                                                  Value = 0,
                                                  Width = 1,
                                                  Color = ColorTranslator.FromHtml("#808080")
                                              }
                                          }
                })
                .SetTooltip(new Tooltip
                {
                    Formatter = @"function() {
                                        return '<b>'+ this.series.name +'</b><br/>'+
                                    this.x +': '+ this.y +'°C';
                                }"
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Right,
                    VerticalAlign = VerticalAligns.Top,
                    X = -10,
                    Y = 100,
                    BorderWidth = 0
                })
                .SetSeries(new[]
                           {
                               new Series {Name = "MaxTemp", Data= minTemp},
                               new Series {Name = "MinTemp", Data= maxTemp},
                               new Series {Name = "RainFall", Data= rainFall},
                           }
                );
            return chart;
        }

        private Highcharts BasicLineMultipleStation(string chartHeading, PairModel station1, PairModel station2, PairModel station3, PairModel station4, string[] categories)
        {
            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart
                {
                    DefaultSeriesType = ChartTypes.Line,
                    MarginRight = 130,
                    MarginBottom = 25,
                    ClassName = "chart"
                })
                .SetTitle(new Title
                {
                    Text = chartHeading,
                    X = -20
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Source: Dummy Data",
                    X = -20
                })
                .SetXAxis(new XAxis { Categories = categories })
                .SetYAxis(new YAxis
                {
                    Title = new XAxisTitle { Text = "Temperature (°C)" },
                    PlotLines = new[]
                                          {
                                              new XAxisPlotLines
                                              {
                                                  Value = 0,
                                                  Width = 1,
                                                  Color = ColorTranslator.FromHtml("#808080")
                                              }
                                          }
                })
                .SetTooltip(new Tooltip
                {
                    Formatter = @"function() {
                                        return '<b>'+ this.series.name +'</b><br/>'+
                                    this.x +': '+ this.y +'°C';
                                }"
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Right,
                    VerticalAlign = VerticalAligns.Top,
                    X = -10,
                    Y = 100,
                    BorderWidth = 0
                })
                .SetSeries(new[]
                           {
                               new Series {Name = station1.Key.ToString(), Data= (Data)station1.Value},
                               new Series {Name = station2.Key.ToString(), Data= (Data)station2.Value},
                               new Series {Name = station3.Key.ToString(), Data= (Data)station3.Value},
                               new Series {Name = station4.Key.ToString(), Data= (Data)station4.Value},
                           }
                );
            return chart;
        }
    }
}
