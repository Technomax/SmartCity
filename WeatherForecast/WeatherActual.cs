using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCityEntity;

namespace WeatherForecast
{
    class WeatherActual
    {
        private List<WeatherSamples> samples = new List<WeatherSamples>();
        private int inputSize;
        private int outputSize;


        //set input size and output size for neurons
        public WeatherActual(int inputSize, int outputSize)
        {
            this.inputSize = inputSize;
            this.outputSize = outputSize;
        }

        //retutn inputset for prediction.
        public void getInputDataToPredict( DateTime date, double[] input,double[] predict=null,double airPollutionLevel=0) {
            SmartCityEntities _dbo = new SmartCityEntities();
          
                var tempinput = (from a in _dbo.fc_PreditInput(date, "Kathmandu")
                                 select new WeatherSamples
                                        {
                                            date = a.MeaureDate,
                                            station = a.Station,
                                            airPollutionLevel = a.AirPolutionLevel,
                                            rainfall = a.RainFall,
                                            maxTemp = a.MaxTemp,
                                            minTemp = a.MinTemp
                                        }).ToList();
            
           
            for (int i = 0; i < this.inputSize; i++)
            {
                WeatherSamples sample = (WeatherSamples)tempinput[i];
                input[i] = sample.airPollutionLevel;
                input[i + this.inputSize] = sample.minTemp;
                input[i + this.inputSize * 2] = sample.maxTemp;
                input[i + this.inputSize * 3] = sample.rainfall;
            }
        }

        public void getInputDataToPredictSeries(DateTime date, double[] input, List<double[]> predictHistory, double airPollutionLevel = 0)
        {

            SmartCityEntities _dbo = new SmartCityEntities();

            var tempinput = (from a in _dbo.fc_PreditInput(date, "Kathmandu")
                             select new WeatherSamples
                             {
                                 date = a.MeaureDate,
                                 station = a.Station,
                                 airPollutionLevel = a.AirPolutionLevel,
                                 rainfall = a.RainFall,
                                 maxTemp = a.MaxTemp,
                                 minTemp = a.MinTemp
                             }).ToList();
            
            foreach (double[] predict in predictHistory) {
              
                tempinput.RemoveAt(0);
                 tempinput.Add(new WeatherSamples
                {
                    date = tempinput[8].date.AddDays(+1),
                    station = tempinput[8].station,
                    airPollutionLevel = tempinput[8].airPollutionLevel,
                    rainfall = predict[2],
                    maxTemp = predict[1],
                    minTemp = predict[0]
                });
                
            }
            
            
         
               
            
            for (int i = 0; i < this.inputSize; i++)
            {
                WeatherSamples sample = (WeatherSamples)tempinput[i];
                input[i] = sample.airPollutionLevel;
                input[i + this.inputSize] = sample.minTemp;
                input[i + this.inputSize * 2] = sample.maxTemp;
                input[i + this.inputSize * 3] = sample.rainfall;
            }
        }



        //return input set as input
        public void getInputData(int offset, double[] input)
        {
            Object[] samplesArray = this.samples.ToArray();
            // get weather data
            for (int i = 0; i < this.inputSize; i++)
            {
                WeatherSamples sample = (WeatherSamples)samplesArray[offset+ i];
                input[i] = sample.airPollutionLevel;
                input[i + this.inputSize] = sample.minTemp;
                input[i + this.inputSize*2] = sample.maxTemp;
                input[i + this.inputSize*3] = sample.rainfall;
            }
        }

        //return input set as output
        public void getOutputData(int offset, double[] output)
        {
            Object[] samplesArray = this.samples.ToArray();
            for (int i = 0; i < this.outputSize; i++)
            {
                WeatherSamples sample = (WeatherSamples)samplesArray[offset + this.inputSize + i];
                output[i] = sample.minTemp;
                output[i+this.outputSize] = sample.maxTemp;
                output[i+this.outputSize*2] = sample.rainfall;
            }

        }

        public int size()
        {
            return this.samples.Count;
        }

        /**
         * @return the samples
         */
        public IList<WeatherSamples> getSamples()
        {
            return this.samples;
        }
        //Datebase connection to get the past records.
        public void load() {

           SmartCityEntities _dbo = new SmartCityEntities();

           this.samples = (from a in _dbo.Adm_Weather 
                        select new WeatherSamples 
            { date = (DateTime)a.WeatherDate, station=a.Station,airPollutionLevel=a.AirPolutionLevel, rainfall=a.RainFall, maxTemp=a.MaxTemp, minTemp=a.MinTemp }).ToList();
        }
    }
}
