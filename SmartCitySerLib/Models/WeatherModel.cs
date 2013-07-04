using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SmartCityServiceLibrary.Models
{
    [DataContract]
    public class WeatherResultModel
    {
        [DataMember(Name = "Date")]
        public DateTime WeatherDate { get; set; }
        [DataMember(Name = "Station")]
        public string StationName { get; set; }
        [DataMember(Name = "MinTemp")]
        public double MinTemp { get; set; }
        [DataMember(Name = "MaxTemp")]
        public double MaxTemp { get; set; }
        [DataMember(Name = "RainFall")]
        public double RainFall { get; set; }
    }

    [DataContract]
    public class TabularModel
    {
        [DataMember(Name = "WeatherDate")]
        public string WeatherDate { get; set; }
        [DataMember(Name = "Station")]
        public string Station { get; set; }
        [DataMember(Name = "AirPolutionLevel")]
        public double AirPolutionLevel { get; set; }
        [DataMember(Name = "MinTemp")]
        public double MinTemp { get; set; }
        [DataMember(Name = "MaxTemp")]
        public double MaxTemp { get; set; }
        [DataMember(Name = "RainFall")]
        public double RainFall { get; set; }
    }
}