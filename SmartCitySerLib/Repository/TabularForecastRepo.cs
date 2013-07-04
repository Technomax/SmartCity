using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartCityServiceLibrary.Models;
using SmartCityEntity;
using Forecasting;



namespace SmartCityServiceLibrary.Repository
{
    public static class TabularForecastRepo
    {
        public static IEnumerable<TabularModel> GetTabularForecast(string stationName, DateTime setDate, double airPollutionLevel)
        {

            PredictWeather obj = new PredictWeather();
            var list = (from c in obj.prediction(setDate, stationName, airPollutionLevel)
                        select new TabularModel
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