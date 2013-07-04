using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.Models.Global
{
    public class PairModel
    {
        public object Key { get; set; }
        public object Value { get; set; }
    }

    public class HighchartModel
    {
        public DotNet.Highcharts.Highcharts highChart { get; set; }
    }

    public class BaseLineSearchModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Station { get; set; }
        public string DisplayOption { get; set; }
        public string ChartOption { get; set; }
        public string StationList { get; set; }
        public string WeatherParameter { get; set; }
        public IEnumerable<PairModel> ddlChartOption { get; set; }
        public IEnumerable<PairModel> ddlMeasureOption { get; set; }
        public IEnumerable<PairModel> ddlStation { get; set; }
        public IEnumerable<PairModel> ddlWeatherParameter { get; set; }
    }

    public class ForecastSearchModel {

        public DateTime SetDate { get; set; }
        public string Station { get; set; }
        public double airPollutionLevel
        {
            get;
            set;
        }
        public IEnumerable<PairModel> ddlStation { get; set; }
    }
}