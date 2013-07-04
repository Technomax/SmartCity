using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartCityEntity;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using System.Drawing;

namespace SmartCity.Repository
{
    public static class DualAxesLineAndColumn
    {
        public static object[] GetDataForDualAxesLineAndColumnChart(string stationName, DateTime fromDate, DateTime toDate, string displayMode, string tempParam)
        {
            SmartCityEntities db = new SmartCityEntities();
            switch (tempParam)
            {
                case "AvgTemp":
                    return db.fc_DualAxesLineAndColumn(stationName, fromDate, toDate, displayMode).Select(x => x.AvgTemp).ToArray().Cast<object>().ToArray();
                case "AvgRainFall":
                    return db.fc_DualAxesLineAndColumn(stationName, fromDate, toDate, displayMode).Select(x => x.AvgRainFall).ToArray().Cast<object>().ToArray();
                default:
                    return null;
            }
        }
    }
}