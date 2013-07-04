using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartCityEntity;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using System.Drawing;

namespace SmartCity.Repository
{
    public static class ColumnLineAndPieRepo
    {
        public static string[] GetCategoryArray(string stationName)
        {
            return stationName.Split(',');
        }

        public static Series[] GetSeriesArray(string stationName, DateTime fromDate, DateTime toDate)
        {
            SmartCityEntities db = new SmartCityEntities();
            var rawData = db.sp_GetDataForColLineAndPie(stationName, fromDate, toDate).Select(x => x).ToArray();
            string[] measureParam=new string[] {"AirPollution","MinTemp","MaxTemp","RainFall"};
            Series[] seriesArray = new Series[rawData.Count()];
            for (int i = 0; i < rawData.Count(); i++)
            {
                sp_GetDataForColLineAndPie_Result tempObj = ((sp_GetDataForColLineAndPie_Result)rawData.GetValue(i));
                seriesArray[i] = new Series
                {
                    Type = i<rawData.Count()-1?ChartTypes.Column:ChartTypes.Spline,
                    Name = tempObj.Station,
                    Data = new Data(new object[]
                        {
                            tempObj.AvgAirPollutionLevel, tempObj.AvgMinTemp, tempObj.AvgMaxTemp, tempObj.AvgRainFall
                         }
                     )
                };
            }

            Series[] series = new[]
                           {
                               new Series
                               {
                                   Type = ChartTypes.Column,
                                   Name = "Kathmandu",
                                   Data = new Data(new object[] { 3, 2, 1, 3 })
                               },
                               new Series
                               {
                                   Type = ChartTypes.Column,
                                   Name = "Pokhara",
                                   Data = new Data(new object[] { 2, 3, 5, 7 })
                               },
                               new Series
                               {
                                   Type = ChartTypes.Column,
                                   Name = "Butwal",
                                   Data = new Data(new object[] { 4, 3, 3, 9 })
                               },
                               new Series
                               {
                                   Type = ChartTypes.Column,
                                   Name = "Bhairahawa",
                                   Data = new Data(new object[] { 3, 2.67, 3, 6.33})
                               }//,
                               // new Series
                               //{
                               //    Type = ChartTypes.Spline,
                               //    Name = "Average",
                               //    Data = new Data(new object[] { 3, 2.67, 3, 6.33, 3.33 })
                               //}//,
                               //new Series
                               //{
                               //    Type = ChartTypes.Pie,
                               //    Name = "Total consumption",
                               //    Data = new Data(new[]
                               //                    {
                               //                        new DotNet.Highcharts.Options.Point
                               //                        {
                               //                            Name = "Jane",
                               //                            Y = 13,
                               //                            Color = Color.FromName("Highcharts.getOptions().colors[0]")
                               //                        },
                               //                        new DotNet.Highcharts.Options.Point
                               //                        {
                               //                            Name = "John",
                               //                            Y = 23,
                               //                            Color = Color.FromName("Highcharts.getOptions().colors[1]")
                               //                        },
                               //                        new DotNet.Highcharts.Options.Point
                               //                        {
                               //                            Name = "Joe",
                               //                            Y = 19,
                               //                            Color = Color.FromName("Highcharts.getOptions().colors[2]")
                               //                        }
                               //                    }
                               //        )
                               //}
                           };
            return seriesArray;
        }
    }
}