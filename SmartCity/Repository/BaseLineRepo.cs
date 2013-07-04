using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forecasting;
using SmartCityEntity;


namespace SmartCity.Repository
{
    public static class BaseLineRepo
    {
        public static object[] GetMonlyAvgSeriesForAGivenYear(string stationName, int year, string param)
        {
            SmartCityEntities db = new SmartCityEntities();
            switch (param)
            {
                case "AirPolutionLevel":
                    return db.Adm_Weather.Where(x => x.Station == stationName && x.WeatherDate.Year == year).Select(x => x.AirPolutionLevel).Take(12).ToArray().Cast<object>().ToArray();
                case "MinTemp":
                    return db.Adm_Weather.Where(x => x.Station == stationName && x.WeatherDate.Year == year).Select(x => x.MinTemp).Take(12).ToArray().Cast<object>().ToArray();
                case "MaxTemp":
                    return db.Adm_Weather.Where(x => x.Station == stationName && x.WeatherDate.Year == year).Select(x => x.MaxTemp).Take(12).ToArray().Cast<object>().ToArray();
                case "RainFall":
                    return db.Adm_Weather.Where(x => x.Station == stationName && x.WeatherDate.Year == year).Select(x => x.RainFall).Take(12).ToArray().Cast<object>().ToArray();
                default:
                    return null;
            }
        }
        public static object[] GetDataForBaseLineChart(string stationName, DateTime fromDate, DateTime toDate, string displayMode, string compareParam)
        {
            SmartCityEntities db = new SmartCityEntities();
            switch (compareParam)
            {
                case "AirPolutionLevel":
                    return db.fc_BaseLine(stationName, fromDate, toDate, displayMode).Select(x => x.AirPolutionLevel).ToArray().Cast<object>().ToArray();
                case "MinTemp":
                    return db.fc_BaseLine(stationName, fromDate, toDate, displayMode).Select(x => x.MinTemp).ToArray().Cast<object>().ToArray();
                case "MaxTemp":
                    return db.fc_BaseLine(stationName, fromDate, toDate, displayMode).Select(x => x.MaxTemp).ToArray().Cast<object>().ToArray();
                case "RainFall":
                    return db.fc_BaseLine(stationName, fromDate, toDate, displayMode).Select(x => x.RainFall).ToArray().Cast<object>().ToArray();
                default:
                    return null;
            }
        }


        public static object[][] GetDataForForecastBaseLineChart(string stationName, DateTime setDate, double airPollutionLevel)
        {
            object[][] value = new object[4][];
            for (int x = 0; x < value.Length; x++) { 
            
            value[x]=new object[30];
            }
            PredictWeather obj = new PredictWeather();
            var list = obj.prediction(setDate, stationName, airPollutionLevel);

            var i = 0;
            foreach (var item in list)
            {
                value[0][i] = item.minTemp;
                value[1][i] = item.maxTemp;
                value[2][i] = item.rainfall;
                i++;

            }
            return value;
        }
    }
}