using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartCityEntity;


namespace SmartCity.Repository
{
    public static class DashboardRepo
    {
        public static string[] GetCategories()
        {
            return new string[] { DateTime.Now.DayOfWeek.ToString(), DateTime.Now.AddDays(-1).DayOfWeek.ToString(), 
                DateTime.Now.AddDays(-2).DayOfWeek.ToString(), DateTime.Now.AddDays(-3).DayOfWeek.ToString(),
                DateTime.Now.AddDays(-4).DayOfWeek.ToString(),DateTime.Now.AddDays(-5).DayOfWeek.ToString(),DateTime.Now.AddDays(-6).DayOfWeek.ToString()};
        }

        public static object[] GetDataForDashboard(string stationName, DateTime fromDate, DateTime toDate, string compareParam)
        {
            SmartCityEntities db = new SmartCityEntities();
            switch (compareParam)
            {
                case "AirPolutionLevel":
                    return db.fc_GetLast7DayData(stationName, fromDate, toDate).Select(x => x.AirPolutionLevel).ToArray().Cast<object>().ToArray();
                case "MinTemp":
                    return db.fc_GetLast7DayData(stationName, fromDate, toDate).Select(x => x.MinTemp).ToArray().Cast<object>().ToArray();
                case "MaxTemp":
                    return db.fc_GetLast7DayData(stationName, fromDate, toDate).Select(x => x.MaxTemp).ToArray().Cast<object>().ToArray();
                case "RainFall":
                    return db.fc_GetLast7DayData(stationName, fromDate, toDate).Select(x => x.RainFall).ToArray().Cast<object>().ToArray();
                default:
                    return null;
            }
        }
    }
}