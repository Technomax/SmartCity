using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SmartCityServiceLibrary.Models;
using SmartCityServiceLibrary.Repository;

namespace SmartCityServiceLibrary
{
    public class Service1 : IService1
    {
        public List<WeatherResultModel> GetWeather()
        {
            List<WeatherResultModel> weatherModel = new List<WeatherResultModel>();
            return WeatherRepo.GetWeathers().ToList();
        }
    }
}
