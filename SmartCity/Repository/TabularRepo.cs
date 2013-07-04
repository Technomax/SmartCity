using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartCityEntity;
using SmartCity.Models;
using Forecasting;


namespace SmartCity.Repository
{
    public static class TabularRepo
    {
        public static IEnumerable<TabularModel> GetTabularReport(string stationName, DateTime fromDate, DateTime toDate, string displayMode, string compareParam = "")
        {
            SmartCityEntities db = new SmartCityEntities();
            switch (compareParam)
            {
                case "AirPolutionLevel":
                    var a = (from c in db.fc_rpt_Tabular(stationName, fromDate, toDate, displayMode)
                             select new TabularModel
                             {
                                 WeatherDate = c.MeaureDate,
                                 Station=c.Station,
                                 AirPolutionLevel = c.AirPolutionLevel
                             }
                                ).ToList();
                    return (from c in db.fc_rpt_Tabular(stationName, fromDate, toDate, displayMode)
                            select new TabularModel
                            {
                                WeatherDate = c.MeaureDate,
                                Station=c.Station,
                                AirPolutionLevel = c.AirPolutionLevel
                            }
                                ).ToList();
                case "MinTemp":
                    return (from c in db.fc_rpt_Tabular(stationName, fromDate, toDate, displayMode)
                            select new TabularModel
                            {
                                WeatherDate = c.MeaureDate,
                                Station = c.Station,
                                MinTemp = c.MinTemp
                            }
                                ).ToList();
                case "MaxTemp":
                    return (from c in db.fc_rpt_Tabular(stationName, fromDate, toDate, displayMode)
                            select new TabularModel
                            {
                                WeatherDate = c.MeaureDate,
                                Station = c.Station,
                                MaxTemp = c.MaxTemp
                            }
                                ).ToList();
                case "RainFall":
                    return (from c in db.fc_rpt_Tabular(stationName, fromDate, toDate, displayMode)
                            select new TabularModel
                            {
                                WeatherDate = c.MeaureDate,
                                Station = c.Station,
                                RainFall = c.RainFall
                            }
                                ).ToList();
                default:
                    return (from c in db.fc_rpt_Tabular(stationName, fromDate, toDate, displayMode)
                            select new TabularModel
                            {
                                WeatherDate = c.MeaureDate,
                                Station = c.Station,
                                AirPolutionLevel = c.AirPolutionLevel,
                                MaxTemp = c.MaxTemp,
                                MinTemp = c.MinTemp,
                                RainFall = c.RainFall
                            }
                                ).ToList();
            }
        }

        public static IEnumerable<TabularModel> GetTabularForecast(string stationName, DateTime setDate, double airPollutionLevel) {

            PredictWeather obj = new PredictWeather();
            var list=( from c in obj.prediction(setDate,stationName,airPollutionLevel)  select new TabularModel
                            {
                                WeatherDate = c.date.ToString(),
                                Station = c.station,
                                AirPolutionLevel = c.airPollutionLevel,
                                MaxTemp = c.maxTemp,
                                MinTemp = c.minTemp,
                                RainFall = c.rainfall
                            }
                                ).ToList();
            return list;
        }
    }
}