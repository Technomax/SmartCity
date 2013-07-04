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
        [DataMember(Name = "Station")]
        public string StationName { get; set; }
        [DataMember(Name = "MinTemp")]
        public int MinTemp { get; set; }
        [DataMember(Name = "MaxTemp")]
        public int MaxTemp { get; set; }
        [DataMember(Name = "RainFall")]
        public int RainFall { get; set; }
    }
}