using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartCity.Models.Global;

namespace SmartCity.Repository
{
    public class PairRepo
    {
        public static List<PairModel> GetPairModel(string param)
        {
            List<PairModel> _pairModel = new List<PairModel>();
            switch (param)
            {
                case "BaseLine_Option":
                    _pairModel.Add(new PairModel { Key = "Single Station", Value = "Single Station" });
                    _pairModel.Add(new PairModel { Key = "Single Parameter", Value = "Single Parameter" });
                    return _pairModel;
                case "BaseLine_Option_Tabular":
                    _pairModel.Add(new PairModel { Key = "Multiple Station", Value = "Single Parameter" });
                    _pairModel.Add(new PairModel { Key = "Single Station", Value = "Single Station" });
                    return _pairModel;
                case "WeatherParameter":
                    _pairModel.Add(new PairModel { Key = "AirPolutionLevel", Value = "Air Pollution" });
                    _pairModel.Add(new PairModel { Key = "MinTemp", Value = "Min Temperature" });
                    _pairModel.Add(new PairModel { Key = "MaxTemp", Value = "Max Temperature" });
                    _pairModel.Add(new PairModel { Key = "RainFall", Value = "RainFall" });
                    return _pairModel;
                case "BaseLine_CompareType":
                    _pairModel.Add(new PairModel { Key = "Monthly", Value = "Monthly" });
                    _pairModel.Add(new PairModel { Key = "Weekly", Value = "Weekly" });
                    _pairModel.Add(new PairModel { Key = "Yearly", Value = "Yearly" });
                    return _pairModel;
                case "Station":
                    _pairModel.Add(new PairModel { Key = "Kathmandu", Value = "Kathmandu" });
                    _pairModel.Add(new PairModel { Key = "Pokhara", Value = "Pokhara" });
                    _pairModel.Add(new PairModel { Key = "Birgung", Value = "Birgung" });
                    _pairModel.Add(new PairModel { Key = "Butwal", Value = "Butwal" });
                    _pairModel.Add(new PairModel { Key = "Jhapa", Value = "Jhapa" });
                    _pairModel.Add(new PairModel { Key = "Butwal", Value = "Butwal" });
                    _pairModel.Add(new PairModel { Key = "Bhairahawa", Value = "Bhairahawa" });
                    _pairModel.Add(new PairModel { Key = "Palpa", Value = "Palpa" });
                    _pairModel.Add(new PairModel { Key = "Jiri", Value = "Jiri" });
                    _pairModel.Add(new PairModel { Key = "Biratnagar", Value = "Biratnagar" });                    
                    return _pairModel;
                default:
                    return _pairModel;
            }
        }
    }
}