using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.Models
{
    public class BaseLineModel
    {
        public DotNet.Highcharts.Highcharts highChart { get; set; }
        public SmartCity.Models.Global.BaseLineSearchModel searchParam { get; set; }
    }
    public class BaseLinePredictModel
    {
        public DotNet.Highcharts.Highcharts highChart { get; set; }
        public SmartCity.Models.Global.ForecastSearchModel searchParam { get; set; }
    }


}