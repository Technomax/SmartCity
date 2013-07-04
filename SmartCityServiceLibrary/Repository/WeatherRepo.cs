using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartCityServiceLibrary.Models;
using SmartCityEntity;


namespace SmartCityServiceLibrary.Repository
{
    public static class WeatherRepo
    {
        #region Weather
        public static IEnumerable<WeatherResultModel> GetWeathers()
        {
            SmartCityEntities db = new SmartCityEntities();
            return from c in db.Adm_Weather select new WeatherResultModel { StationName=c.Station,MinTemp=c.MinTemp,MaxTemp=c.MaxTemp,RainFall=c.RainFall};
        }
        #endregion
    }
}