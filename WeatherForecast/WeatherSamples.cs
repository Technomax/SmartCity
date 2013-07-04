using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNHelpers.Util;

namespace WeatherForecast
{
    class WeatherSamples:IComparable<WeatherSamples>
    {
        public DateTime date;
        public string station;
        public double airPollutionLevel;
        public double minTemp;
        public double maxTemp;
        public double rainfall;

        public int CompareTo(WeatherSamples other)
        {
            return getDate().CompareTo(other.getDate());
        }


        //Get the properties
        public DateTime getDate() {
            return this.date;
        }
        public double getMinTemp() {
            return this.minTemp;
        }

        public double getMaxTemp()
        {
            return this.maxTemp;
        }
        public double getAirPollutionLevel()
        {
            return this.airPollutionLevel;
        }
        public double getRainfall()
        {
            return this.rainfall;
        }

        //set the properties
        public void setDate(DateTime date) {
            this.date = date;
        }

        public void setMinTemp(double minTemp) {

            this.minTemp = minTemp;
        }
        public void setMaxTemp(double maxTemp) {
            this.maxTemp = maxTemp;
        }
        public void setRainfall(double rainfall) {

            this.rainfall = rainfall;
        }




        override public String ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(ReadCSV.DisplayDate(this.date));
            result.Append(", Air Pollution: ");
            result.Append(this.airPollutionLevel);
            result.Append(", Min Temp: ");
            result.Append(this.minTemp.ToString("N2"));
            result.Append(", Max Temp: ");
            result.Append(this.maxTemp.ToString("N2"));
            result.Append(", Rainfall: ");
            result.Append(this.rainfall.ToString("N2"));
            return result.ToString();
        }


    }
}
