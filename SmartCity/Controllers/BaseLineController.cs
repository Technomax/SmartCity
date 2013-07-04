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
    public class BaseLineController : Controller
    {
        public ActionResult Index()
        {
            return View(new SmartCity.Models.BaseLineModel
            {
                highChart = BasicLineSingleStation(
                                      "Monthly Average AirPollution, MinTemp, MaxTemp, and RainFall",   
                                      new Data(BaseLineRepo.GetDataForBaseLineChart("Kathmandu", DateTime.Now.AddYears(-1),DateTime.Now,"Monthly", "AirPolutionLevel")),
                                      new Data(BaseLineRepo.GetDataForBaseLineChart("Kathmandu", DateTime.Now.AddYears(-1), DateTime.Now, "Monthly", "MinTemp")),
                                      new Data(BaseLineRepo.GetDataForBaseLineChart("Kathmandu", DateTime.Now.AddYears(-1), DateTime.Now, "Monthly", "MaxTemp")),
                                      new Data(BaseLineRepo.GetDataForBaseLineChart("Kathmandu", DateTime.Now.AddYears(-1), DateTime.Now, "Monthly", "RainFall")),
                                      CustomChartsData.CategoryMonth
                                      ),
                searchParam = new SmartCity.Models.Global.BaseLineSearchModel
                {
                    FromDate=DateTime.Today.AddYears(-1),
                    ToDate=DateTime.Today,
                    ddlChartOption = PairRepo.GetPairModel("BaseLine_Option"),
                    ddlMeasureOption = PairRepo.GetPairModel("BaseLine_CompareType"),
                    ddlStation = PairRepo.GetPairModel("Station"),
                    StationList = "Kathmandu",
                    ddlWeatherParameter = PairRepo.GetPairModel("WeatherParameter")
                }
            });
        }
       
        public ActionResult _Chart(DateTime paramFromDate, DateTime paramToDate, string station, string displayOption, string chartOption, string tempParam)
        {
            var a = new Data(BaseLineRepo.GetDataForBaseLineChart(station, paramFromDate, paramToDate, displayOption, "AirPolutionLevel"));
            if (chartOption == "Single Station")
            {
                return PartialView(new SmartCity.Models.BaseLineModel
                {
                    highChart = BasicLineSingleStation(
                                          displayOption+ " Average AirPollution, MinTemp, MaxTemp, and RainFall",   
                                          new Data(BaseLineRepo.GetDataForBaseLineChart(station, paramFromDate, paramToDate, displayOption, "AirPolutionLevel")),
                                          new Data(BaseLineRepo.GetDataForBaseLineChart(station, paramFromDate, paramToDate, displayOption, "MinTemp")),
                                          new Data(BaseLineRepo.GetDataForBaseLineChart(station, paramFromDate, paramToDate, displayOption, "MaxTemp")),
                                          new Data(BaseLineRepo.GetDataForBaseLineChart(station, paramFromDate, paramToDate, displayOption, "RainFall")),
                                          displayOption == "Yearly" ? CustomChartsData.CategoryYear : displayOption == "Weekly" ? CustomChartsData.CategoryWeek : CustomChartsData.CategoryMonth
                                          )
                });
            }
            else
            {
                string[] stationList = station.Split(',');
                return PartialView(new SmartCity.Models.BaseLineModel
                {
                    highChart = BasicLineMultipleStation(
                                          displayOption+ " " + tempParam,   
                                          new PairModel { Key = stationList[0], Value = new Data(BaseLineRepo.GetDataForBaseLineChart(stationList[0], paramFromDate, paramToDate, displayOption, tempParam)) },
                                          new PairModel { Key = stationList[1], Value = new Data(BaseLineRepo.GetDataForBaseLineChart(stationList[1], paramFromDate, paramToDate, displayOption, tempParam)) },
                                          new PairModel { Key = stationList[2], Value = new Data(BaseLineRepo.GetDataForBaseLineChart(stationList[2], paramFromDate, paramToDate, displayOption, tempParam)) },
                                          new PairModel { Key = stationList[3], Value = new Data(BaseLineRepo.GetDataForBaseLineChart(stationList[3], paramFromDate, paramToDate, displayOption, tempParam)) },
                                          displayOption == "Yearly" ? CustomChartsData.CategoryYear : displayOption == "Weekly" ? CustomChartsData.CategoryWeek : CustomChartsData.CategoryMonth
                                          )
                });
            }
        }

        private Highcharts BasicLineSingleStation(string chartHeading, Data airPollutionLevel, Data minTemp, Data maxTemp, Data rainFall, string[] categories)
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
                               new Series {Name = "AirPolutionlevel", Data= airPollutionLevel},
                               new Series {Name = "MaxTemp", Data= minTemp},
                               new Series {Name = "MinTemp", Data= maxTemp},
                               new Series {Name = "RainFall", Data= rainFall},
                           }
                );
            return chart;
        }

        private Highcharts BasicLineMultipleStation(string chartHeading,PairModel station1, PairModel station2, PairModel station3, PairModel station4, string[] categories)
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
