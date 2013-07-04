using System;
using System.Drawing;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Point = DotNet.Highcharts.Options.Point;

namespace SmartCity.Samples.Models
{
    public class CustomChartsData
    {
        public static string[] CategoryMonth = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        public static string[] CategoryYear = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        public static string[] CategoryWeek = new[] { "23", "24", "25", "26", "26", "28", "29", "30", "31", "32", "33", "34",
                                                      "33", "34", "35", "36", "36", "38", "39", "40", "41", "42", "43", "44",
                                                      "43", "44", "45", "46", "46", "48", "49", "50", "51", "52", "53", "54"};
        public static string[] CategoryDays = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12",
                                                      "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24",
                                                      "25", "26", "27", "28", "29", "30"};
    }
}