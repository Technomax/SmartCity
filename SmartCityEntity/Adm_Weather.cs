//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartCityEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Adm_Weather
    {
        public long IDWeather { get; set; }
        public string Station { get; set; }
        public System.DateTime WeatherDate { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public double RainFall { get; set; }
        public double AirPolutionLevel { get; set; }
    }
}
