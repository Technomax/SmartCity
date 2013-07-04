using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartCity.Models.Global;

namespace SmartCity.Models
{
        public class TabularModel
        {
            public string WeatherDate { get; set; }
            public string Station { get; set; }
            public double AirPolutionLevel { get; set; }
            public double MinTemp { get; set; }
            public double MaxTemp { get; set; }
            public double RainFall { get; set; }
        }

        public class TabularModel_Extended
        {
            public IEnumerable<TabularModel> tabularModel { get; set; }
            public SmartCity.Models.Global.BaseLineSearchModel searchParam { get; set; }
        }

        public class TabularModel_Forecast {
            public IEnumerable<TabularModel> tabularModel { get; set; }
            public SmartCity.Models.Global.ForecastSearchModel searchParam { get; set; }        
        }
}