using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SmartCityServiceLibrary.Models;
using SmartCityServiceLibrary.Repository;

namespace SmartCitySerLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public List<WeatherResultModel> GetWeather()
        {
            List<WeatherResultModel> weatherModel = new List<WeatherResultModel>();
            return WeatherRepo.GetWeathers().ToList();
        }

        public List<TabularModel> GetWeatherForecast(float id)
        {
            return WeatherRepo.GetTabularForecast("Kathmandu", DateTime.Now, id).ToList();
        }

        public List<WeatherResultModel> GetWeatherData1()
        {
            return WeatherRepo.GetWeathers().Take(1).ToList();
        }
    }
}
