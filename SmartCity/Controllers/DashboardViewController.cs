using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
using SmartCity.Repository;
using Point = DotNet.Highcharts.Options.Point;
using SmartCity.Models.Global;

namespace SmartCity.Controllers
{
    public class DashboardViewController : Controller
    {
        public ActionResult Index()
        {
            return View(DataLabels(
                                      "Daily Min Temperature",
                                      DashboardRepo.GetCategories(),
                                       new PairModel { Key = "Kathmandu", Value = new Data(DashboardRepo.GetDataForDashboard("Kathmandu", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MinTemp")) },
                                      new PairModel { Key = "Pokhara", Value = new Data(DashboardRepo.GetDataForDashboard("Pokhara", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MinTemp")) },
                                      new PairModel { Key = "Palpa", Value = new Data(DashboardRepo.GetDataForDashboard("Palpa", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MinTemp")) },
                                      new PairModel { Key = "Butwal", Value = new Data(DashboardRepo.GetDataForDashboard("Butwal", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MinTemp")) }
                                      ));
        }

        public ActionResult AirPollution()
        {
            return PartialView(DataLabels(
                                      "Daily Air Pollution Level",
                                      DashboardRepo.GetCategories(),
                                      new PairModel { Key = "Kathmandu", Value = new Data(DashboardRepo.GetDataForDashboard("Kathmandu", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "AirPolutionLevel")) },
                                      new PairModel { Key = "Pokhara", Value = new Data(DashboardRepo.GetDataForDashboard("Pokhara", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "AirPolutionLevel")) },
                                      new PairModel { Key = "Palpa", Value = new Data(DashboardRepo.GetDataForDashboard("Palpa", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "AirPolutionLevel")) },
                                      new PairModel { Key = "Butwal", Value = new Data(DashboardRepo.GetDataForDashboard("Butwal", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "AirPolutionLevel")) }
                                      ));
        }

        public ActionResult MinTemp()
        {
            return PartialView(DataLabels(
                                      "Daily Max Temperature",
                                      DashboardRepo.GetCategories(),
                                       new PairModel { Key = "Kathmandu", Value = new Data(DashboardRepo.GetDataForDashboard("Kathmandu", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MaxTemp")) },
                                      new PairModel { Key = "Pokhara", Value = new Data(DashboardRepo.GetDataForDashboard("Pokhara", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MaxTemp")) },
                                      new PairModel { Key = "Palpa", Value = new Data(DashboardRepo.GetDataForDashboard("Palpa", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MaxTemp")) },
                                      new PairModel { Key = "Butwal", Value = new Data(DashboardRepo.GetDataForDashboard("Butwal", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MaxTemp")) }
                                      ));
        }

        public ActionResult MaxTemp()
        {
            return PartialView(DataLabels(
                                      "Daily Min Temperature",
                                      DashboardRepo.GetCategories(),
                                       new PairModel { Key = "Kathmandu", Value = new Data(DashboardRepo.GetDataForDashboard("Kathmandu", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MinTemp")) },
                                      new PairModel { Key = "Pokhara", Value = new Data(DashboardRepo.GetDataForDashboard("Pokhara", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MinTemp")) },
                                      new PairModel { Key = "Palpa", Value = new Data(DashboardRepo.GetDataForDashboard("Palpa", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MinTemp")) },
                                      new PairModel { Key = "Butwal", Value = new Data(DashboardRepo.GetDataForDashboard("Butwal", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "MinTemp")) }
                                      ));
        }

        public ActionResult RainFall()
        {
            return PartialView(DataLabels(
                                      "Daily Rainfall",
                                      DashboardRepo.GetCategories(),
                                      new PairModel { Key = "Kathmandu", Value = new Data(DashboardRepo.GetDataForDashboard("Kathmandu", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "RainFall")) },
                                      new PairModel { Key = "Pokhara", Value = new Data(DashboardRepo.GetDataForDashboard("Pokhara", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "RainFall")) },
                                      new PairModel { Key = "Palpa", Value = new Data(DashboardRepo.GetDataForDashboard("Palpa", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "RainFall")) },
                                      new PairModel { Key = "Butwal", Value = new Data(DashboardRepo.GetDataForDashboard("Butwal", DateTime.Now.AddDays(-100), DateTime.Now.AddDays(-94), "RainFall")) }
                                      ));
        }

        private Highcharts DataLabels(string label, string[] categories, PairModel station1, PairModel station2, PairModel station3, PairModel station4)
        {
            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Line })
                .SetTitle(new Title { Text = label })
                .SetSubtitle(new Subtitle { Text = "Source: Dummy Data" })
                .SetXAxis(new XAxis { Categories = categories })
                .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Temperature (°C)" } })
                .SetTooltip(new Tooltip { Enabled = true, Formatter = @"function() { return '<b>'+ this.series.name +'</b><br/>'+ this.x +': '+ this.y +'°C'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Line = new PlotOptionsLine
                    {
                        DataLabels = new PlotOptionsLineDataLabels
                        {
                            Enabled = true
                        },
                        EnableMouseTracking = false
                    }
                })
                .SetSeries(new[]
                           {
                               new Series { Name = station1.Key.ToString(), Data = (Data)station1.Value },
                               new Series { Name = station2.Key.ToString(), Data = (Data)station2.Value  },
                               new Series { Name = station3.Key.ToString(), Data = (Data)station3.Value  },
                               new Series { Name = station4.Key.ToString(), Data = (Data)station3.Value  }
                           });
            return chart;
        }
    }
}
